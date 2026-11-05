using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

// kurserna hämtas från restdb via HttpService. resursen heter "courses"
public class CourseServices : ICourseService
{
    public async Task Add(Course course)
    {
        var httpService = new HttpService<Course>();
        await httpService.Post("courses", course);
    }

    public async Task Delete(string id)
    {
        var httpService = new HttpService<Course>();
        await httpService.Delete("courses", id);
    }

    // hämtar EN kurs på kursnummer. svaret är CourseRestDb så vi får med _id
    public async Task<CourseRestDb> Find(string courseNumber)
    {
        var httpService = new HttpService<CourseRestDb>();
        // bygger json-villkoret som text: {"courseNumber":"CS101"}
        var searchCriteria = "{\"courseNumber\":" + "\"" + courseNumber + "\"}";
        return await httpService.Get("courses", searchCriteria);
    }

    public async Task<List<Course>> List()
    {
        var httpService = new HttpService<Course>();
        return await httpService.Get("courses");
    }
}
