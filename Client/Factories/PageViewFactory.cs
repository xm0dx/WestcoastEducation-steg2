using System;
using Client.Enums;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;

namespace Client.Factories;

public class PageViewFactory : IPageViewFactory
{
    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
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
            // redigeringssidan måste veta vilken lärare den ska visa, därför skickas personnumret med hit
            PageViewsEnum.EditTeacher => new EditTeacherViewModel(new TeacherServices(), id),
            _ => throw new InvalidOperationException("Sidan finns inte")
        };
    }
}
