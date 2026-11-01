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

    // listan hämtas som CourseRestDb så vi får med _id, det behövs för att kunna ta bort
    public async Task<List<CourseRestDb>> List()
    {
        var httpService = new HttpService<CourseRestDb>();
        return await httpService.Get("courses");
    }
}
