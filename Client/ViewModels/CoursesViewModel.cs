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
    private readonly ICourseService _courseService;

    [ObservableProperty]
    public partial ObservableCollection<Course> Courses { get; set; } = [];

    [ObservableProperty]
    public partial bool ShowDialog { get; set; }

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

        Init();
    }

    // ta bort-knappen i listan tar inte bort direkt, den öppnar popupen så användaren får bekräfta
    [RelayCommand]
    private void Delete(string courseNumber)
    {
        _courseNumberToDelete = courseNumber;
        ShowDialog = true;
    }

    [RelayCommand]
    private void Cancel()
    {
        ShowDialog = false;
        _courseNumberToDelete = null;
    }

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
