using Client.Factories;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageViewFactory>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<CoursesViewModel>();
        collection.AddTransient<StudentsViewModel>();
        collection.AddTransient<TeachersViewModel>();

        collection.AddTransient<ICourseService, CourseServices>();
        collection.AddTransient<IStudentService, StudentServices>();
        collection.AddTransient<ITeacherService, TeacherServices>();
    }
}
