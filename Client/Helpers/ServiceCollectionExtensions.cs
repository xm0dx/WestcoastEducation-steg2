using Client.Factories;
using Client.Interfaces;
using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Helpers;

// "this IServiceCollection collection" gör AddServices till en extension method,
// därför kan man skriva collection.AddServices() i App.axaml.cs
public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection collection)
    {
        // AddSingleton: ETT objekt som alla delar så länge appen kör
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<PageViewFactory>();

        // "om någon ber om ett ICourseService, ge dem en CourseServices"
        collection.AddTransient<ICourseService, CourseServices>();
    }
}
