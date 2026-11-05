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

// Hämtar data från restdb, samma klass som i RunStore.
// HttpClient är .NET:s inbyggda klass för webbanrop.
public class HttpService<T> : IDisposable, IHttpService<T> where T : class
{
    // samma json-inställningar som i Storage, svaret från API:t är camelCase
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    // en HttpClient för hela tjänsten i stället för en ny i varje metod.
    // readonly: sätts i konstruktorn och byts inte efter det
    private readonly HttpClient _httpClient;

    // början på adressen, resten ({resource}, id, ?q=) läggs på i varje metod.
    // sätts i konstruktorn från appsettings.json
    private readonly string _baseUrl;

    // konstruktorn körs en gång när tjänsten skapas: läs inställningarna, skapa klienten, sätt headers
    public HttpService()
    {
        // ConfigurationBuilder läser appsettings.json (den kopieras till bin-mappen av csproj).
        // då slipper vi ha adress och nyckel inskrivna i koden
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // GetSection("Settings:BaseUrl") = nyckeln Settings och sen BaseUrl inuti den.
        // .Value är texten, ! = vi lovar kompilatorn att den inte är null
        _baseUrl = config.GetSection("Settings:BaseUrl").Value!;
        var apiKey = config.GetSection("Settings:ApiKey").Value!;

        _httpClient = new()
        {
            BaseAddress = new Uri(_baseUrl)
        };
        _httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        _httpClient.DefaultRequestHeaders.Add("x-apikey", apiKey);
        // talar om för servern att vi vill ha json tillbaka
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    // tar bort ett objekt. kortast av alla: bara adressen med id sist och DeleteAsync
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
        // steg 1. adressen till resursen blir t ex .../rest/products.
        // klienten och headers är redan klara från konstruktorn, så vi skickar direkt
        // obs: inget / sist, .../products/ ger fel 500 från restdb
        var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}");

        // blev det inte 200 OK så ger vi en tom lista i stället för att krascha
        if (!response.IsSuccessStatusCode) return [];

        // steg 5. svaret är text (json), gör om det till en lista med T
        var result = await response.Content.ReadAsStringAsync();
        var list = JsonSerializer.Deserialize<List<T>>(result, _options) ?? [];

        return list;
    }

    // hämtar ETT objekt. restdb filtrerar med ?q= och ett json-villkor i adressen,
    // t ex .../rest/products?q={"itemNumber":"1000"}
    public async Task<T> Get(string resource, string predicate)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}?q={predicate}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(result))
                {
                    throw new Exception("Fanns ingenting");
                }

                // svaret är fortfarande en lista (med ett objekt), så vi tar det första.
                // hittades inget är listan tom, [] i json, och då finns inget [0] att ta
                var data = JsonSerializer.Deserialize<List<T>>(result, _options)!;
                if (data.Count == 0)
                {
                    throw new Exception("Fanns ingenting");
                }
                return data[0];
            }

            // här kastar vi fel i stället för att ge tom lista, den som anropar får fånga det
            throw new Exception("Det gick fel: " + response.StatusCode);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // skickar ett nytt objekt till API:et. PostAsJsonAsync i stället för GetAsync,
    // den gör om objektet till json och skickar det
    public async Task Post(string resource, T data)
    {
        try
        {
            // _options med så att json får små bokstäver (courseNumber), samma som när vi läser
            await _httpClient.PostAsJsonAsync<T>($"{_baseUrl}/{resource}", data, _options);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // uppdaterar ett objekt som redan finns. skillnaden mot Post: id:t läggs sist i adressen,
    // t ex .../rest/products/68f8a1b2c3d4, och vi använder PutAsJsonAsync
    public async Task Put(string resource, T data, string objectId)
    {
        try
        {
            await _httpClient.PutAsJsonAsync<T>($"{_baseUrl}/{resource}/{objectId}", data, _options);
        }
        // nätverksfel för sig, allt annat i den andra
        catch (HttpRequestException ex)
        {
            throw new Exception(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
