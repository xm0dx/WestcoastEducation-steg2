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

    // hämtar EN lärare på personnummer. svaret är TeacherRestDb så vi får med _id
    public async Task<TeacherRestDb> Find(string personalNumber)
    {
        var httpService = new HttpService<TeacherRestDb>();
        var searchCriteria = "{\"personalNumber\":" + "\"" + personalNumber + "\"}";
        return await httpService.Get("teachers", searchCriteria);
    }

    public async Task<List<Teacher>> List()
    {
        var httpService = new HttpService<Teacher>();
        var teachers = await httpService.Get("teachers");
        return teachers;
    }

    // Put byter ut läraren i restdb mot den vi skickar in, _objectId är restdb:s _id
    public async Task Update(Teacher teacher, string _objectId)
    {
        var httpService = new HttpService<Teacher>();
        await httpService.Put("teachers", teacher, _objectId);
    }
}
