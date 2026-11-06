using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ICourseService
{
    Task Add(Course course);
    Task Delete(string id);
    Task<CourseRestDb> Find(string courseNumber);
    Task<List<Course>> List();
}
