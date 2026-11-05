using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

// samma mönster som IProductService i RunStore
public interface ITeacherService
{
    Task Add(Teacher teacher);
    Task<TeacherRestDb> Find(string personalNumber);
    Task<List<Teacher>> List();
    Task Update(Teacher teacher, string _objectId);
}
