using System;
using Client.Enums;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;

namespace Client.Factories;

// factory design pattern: en klass vars enda jobb är att skapa rätt sida.
// MainWindowViewModel säger "Courses" och får en CoursesViewModel tillbaka
public class PageViewFactory : IPageViewFactory
{
    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        // switch som uttryck: "värde => resultat". _ körs om inget passar
        return page switch
        {
            PageViewsEnum.Home => new HomeViewModel(),
            PageViewsEnum.Courses => new CoursesViewModel(new CourseServices()),
            PageViewsEnum.AddCourse => new AddCourseViewModel(new CourseServices()),
            PageViewsEnum.Students => new StudentsViewModel(new StudentServices()),
            PageViewsEnum.AddStudent => new AddStudentViewModel(new StudentServices()),
            PageViewsEnum.Teachers => new TeachersViewModel(new TeacherServices()),
            PageViewsEnum.AddTeacher => new AddTeacherViewModel(new TeacherServices()),
            _ => throw new InvalidOperationException("Sidan finns inte")
        };
    }

    public ViewModelBase LoadPageViewModel(PageViewsEnum page, string id)
    {
        return page switch
        {
            // redigeringssidan måste veta vilken lärare den ska visa, därför skickas id med hit
            PageViewsEnum.EditTeacher => new EditTeacherViewModel(new TeacherServices(), id),
            _ => throw new InvalidOperationException("Sidan finns inte")
        };
    }
}
