using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

// sköter alla anrop mot restdb. HttpClient är .NET:s inbyggda klass för webbanrop
public class HttpService<T> : IDisposable, IHttpService<T> where T : class
{
    // restdb svarar med camelCase (courseNumber), våra egenskaper har stor bokstav (CourseNumber)
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public HttpService()
    {
        // läser appsettings.json så adress och nyckel inte ligger i koden
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _baseUrl = config.GetSection("Settings:BaseUrl").Value!;
        var apiKey = config.GetSection("Settings:ApiKey").Value!;

        _httpClient = new()
        {
            BaseAddress = new Uri(_baseUrl)
        };
        _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        _httpClient.DefaultRequestHeaders.Add("x-apikey", apiKey);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task Delete(string resource, string id)
    {
        try
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{resource}/{id}");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<List<T>> Get(string resource)
    {
        // obs: inget / sist, .../courses/ ger fel från restdb
        var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}");

        if (!response.IsSuccessStatusCode)
        {
            // skriv ut vad restdb svarade, annars blir listan bara tom utan förklaring
            Console.WriteLine($"Hämta {resource} gick fel: {response.StatusCode}");
            return [];
        }

        // svaret är text (json), gör om det till en lista med T
        var result = await response.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<List<T>>(result, _options) ?? [];

        return list;
    }

    // hämtar ETT objekt. restdb filtrerar med ?q= och ett json-villkor,
    // t ex .../rest/courses?q={"courseNumber":"CS101"}
    public async Task<T> Get(string resource, string predicate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}?q={predicate}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<T>>(result, _options) ?? [];

                // svaret är en lista (med ett objekt), så vi tar det första
                if (data.Count == 0)
                {
                    throw new Exception("Fanns ingenting");
                }
                return data[0];
            }

            throw new Exception("Det gick fel: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // PostAsJsonAsync gör om objektet till json och skickar det
    public async Task Post(string resource, T data)
    {
        try
        {
            await _httpClient.PostAsJsonAsync<T>($"{_baseUrl}/{resource}", data, _options);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // skillnaden mot Post: id:t läggs sist i adressen och vi använder Put
    public async Task Put(string resource, T data, string objectId)
    {
        try
        {
            await _httpClient.PutAsJsonAsync<T>($"{_baseUrl}/{resource}/{objectId}", data, _options);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
