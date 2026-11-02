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

    // ObservableCollection säger till listan i vyn när något läggs till eller tas bort
    [ObservableProperty]
    public partial ObservableCollection<CourseRestDb> Courses { get; set; } = [];

    // true = popupen "är du säker?" visas. vyn binder Popup.IsOpen till den här
    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

    // kursen som användaren klickade ta bort på, sparas tills hen svarat i popupen
    private CourseRestDb? _courseToDelete;

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

    // ta bort-knappen i listan tar inte bort direkt, den öppnar popupen så användaren får bekräfta.
    // raden skickar med hela kursen som CommandParameter, då har vi _id direkt
    [RelayCommand]
    private void Delete(CourseRestDb course)
    {
        _courseToDelete = course;
        ShowDialog = true;
    }

    // Nej i popupen: stäng bara rutan
    [RelayCommand]
    private void Cancel()
    {
        ShowDialog = false;
        _courseToDelete = null;
    }

    // Ja i popupen: nu tas kursen bort på riktigt och listan laddas om
    [RelayCommand]
    private async Task Confirm()
    {
        ShowDialog = false;
        if (_courseToDelete is null) return;

        try
        {
            await _courseService.Delete(_courseToDelete._id);
            await LoadCourses();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        _courseToDelete = null;
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
