using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    // interfacet, inte klassen. ViewModeln vet inte var kurserna kommer ifrån
    private readonly ICourseService _courseService;

    // ObservableCollection säger till listan i vyn när något läggs till eller tas bort
    [ObservableProperty]
    public partial ObservableCollection<CourseRestDb> Courses { get; set; } = [];

    // tom konstruktor bara för förhandsvisningen i editorn
    public CoursesViewModel()
    {
        _courseService = new CourseServices();
        PageTitle = "Kurser";
    }

    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Kurser";

        // en konstruktor kan inte vara async, så den startar Init som får vänta i stället
        Init();
    }

    private async void Init()
    {
        await LoadCourses();
    }

    private async Task LoadCourses()
    {
        try
        {
            Courses = new ObservableCollection<CourseRestDb>(await _courseService.List());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
