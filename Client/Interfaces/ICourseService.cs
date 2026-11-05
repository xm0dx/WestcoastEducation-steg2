using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

// löftet för kurstjänsten, samma mönster som IProductService i RunStore
public interface ICourseService
{
    Task Add(Course course);
    Task Delete(string id);
    Task<CourseRestDb> Find(string courseNumber);
    Task<List<Course>> List();
}
