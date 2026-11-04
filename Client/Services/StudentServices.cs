using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;

namespace Client.Services;

// studerande hämtas från restdb, resursen heter "students"
public class StudentServices : IStudentService
{
    public async Task Add(Student student)
    {
        var httpService = new HttpService<Student>();
        await httpService.Post("students", student);
    }

    public async Task<List<StudentRestDb>> List()
    {
        var httpService = new HttpService<StudentRestDb>();
        return await httpService.Get("students");
    }
}
