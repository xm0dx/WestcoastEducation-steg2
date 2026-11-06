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

        // "om någon ber om ett ICourseService, ge dem en CourseServices"
        collection.AddTransient<ICourseService, CourseServices>();
        collection.AddTransient<IStudentService, StudentServices>();
        collection.AddTransient<ITeacherService, TeacherServices>();
    }
}
