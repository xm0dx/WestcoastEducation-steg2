using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Interfaces;

public interface ITeacherService
{
    Task Add(Teacher teacher);
    Task<TeacherRestDb> Find(string id);
    Task<List<TeacherRestDb>> List();
    Task Update(Teacher teacher, string id);
}
