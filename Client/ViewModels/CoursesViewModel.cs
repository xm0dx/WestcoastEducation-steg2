using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class CoursesViewModel : ViewModelBase
{
    // interfacet, inte klassen. ViewModeln vet inte var kurserna kommer ifrån
    private readonly ICourseService _courseService;

    [ObservableProperty]
    public partial ObservableCollection<Course> Courses { get; set; } = [];

    // true = popupen "är du säker?" visas. vyn binder Popup.IsOpen till den här
    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

    // kursnumret som användaren klickade ta bort på, sparas tills hen svarat i popupen
    private string? _courseNumberToDelete;

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

    // ta bort-knappen i listan tar inte bort direkt, den öppnar popupen så användaren får bekräfta
    [RelayCommand]
    private void Delete(string courseNumber)
    {
        _courseNumberToDelete = courseNumber;
        ShowDialog = true;
    }

    // Nej i popupen: stäng bara rutan
    [RelayCommand]
    private void Cancel()
    {
        ShowDialog = false;
        _courseNumberToDelete = null;
    }

    // Ja i popupen: nu händer det på riktigt.
    // knappen skickade kursnumret, men restdb vill ha sitt eget _id för att ta bort.
    // därför: hämta kursen först (Find ger CourseRestDb med _id), ta bort, ladda om listan
    [RelayCommand]
    private async Task Confirm()
    {
        ShowDialog = false;
        if (_courseNumberToDelete is null) return;

        try
        {
            CourseRestDb course = await _courseService.Find(_courseNumberToDelete);
            if (course is not null)
            {
                await _courseService.Delete(course._id);
                await LoadCourses();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        _courseNumberToDelete = null;
    }

    private async void Init()
    {
        await LoadCourses();
    }

    private async Task LoadCourses()
    {
        try
        {
            Courses = new ObservableCollection<Course>(await _courseService.List());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
