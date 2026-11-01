using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

// löftet för kurstjänsten. ViewModels pratar bara med det här, inte med HttpService direkt
public interface ICourseService
{
    Task Add(Course course);
    Task Delete(string id);
    Task<List<CourseRestDb>> List();
}
