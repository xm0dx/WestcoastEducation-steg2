using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

// lärare hämtas från restdb, resursen heter "teachers"
public class TeacherServices : ITeacherService
{
    public async Task Add(Teacher teacher)
    {
        var httpService = new HttpService<Teacher>();
        await httpService.Post("teachers", teacher);
    }

    // hämtar EN lärare på restdb:s _id. villkoret skrivs som json: {"_id":"..."}
    public async Task<TeacherRestDb> Find(string id)
    {
        var httpService = new HttpService<TeacherRestDb>();
        var searchCriteria = "{\"_id\":" + "\"" + id + "\"}";
        return await httpService.Get("teachers", searchCriteria);
    }

    public async Task<List<TeacherRestDb>> List()
    {
        var httpService = new HttpService<TeacherRestDb>();
        return await httpService.Get("teachers");
    }

    // Put byter ut hela läraren i restdb mot den vi skickar in
    public async Task Update(Teacher teacher, string id)
    {
        var httpService = new HttpService<Teacher>();
        await httpService.Put("teachers", teacher, id);
    }
}
