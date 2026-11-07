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

    private string _courseNumber = string.Empty;

    public CoursesViewModel()
    {
        _courseService = new CourseServices();
        PageTitle = "Kurser";
        Init();
    }

    public CoursesViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Kurser";
        Init();
    }

    [RelayCommand]
    private void Delete(string courseNumber)
    {
        _courseNumber = courseNumber;
        ShowDialog = true;
    }

    [RelayCommand]
    private async Task Confirm()
    {
        ShowDialog = false;
        try
        {
            CourseRestDb course = await _courseService.Find(_courseNumber);
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
    }

    [RelayCommand]
    private void Cancel()
    {
        ShowDialog = false;
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
