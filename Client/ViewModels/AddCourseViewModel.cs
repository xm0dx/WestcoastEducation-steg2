using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

// sidan för att lägga till en ny kurs
public partial class AddCourseViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;

    // en egenskap per ruta i formuläret. TextBox.Text binder hit
    [ObservableProperty]
    public partial string CourseNumber { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    [ObservableProperty]
    public partial int Duration { get; set; }

    [ObservableProperty]
    public partial string DurationUnit { get; set; } = "veckor";

    // datumen skrivs som text (2026-01-15) och görs om till DateTime när vi sparar
    [ObservableProperty]
    public partial string StartDate { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string EndDate { get; set; } = string.Empty;

    // CheckBox binder hit. true = distans
    [ObservableProperty]
    public partial bool IsOnline { get; set; }

    [ObservableProperty]
    public partial string TeacherName { get; set; } = string.Empty;

    // visas under formuläret om något är fel, eller när det gick bra
    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;

    public AddCourseViewModel()
    {
        _courseService = new CourseServices();
        PageTitle = "Ny kurs";
    }

    public AddCourseViewModel(ICourseService courseService)
    {
        _courseService = courseService;
        PageTitle = "Ny kurs";
    }

    [RelayCommand]
    private async Task Save()
    {
        // samma kontroller som i inlämning 1, fast nu med ett meddelande i stället för Console
        if (string.IsNullOrWhiteSpace(CourseNumber) || string.IsNullOrWhiteSpace(Title))
        {
            Message = "Kursnummer och titel måste fyllas i.";
            return;
        }

        // TryParse: blir det inget datum så blir det false i stället för en krasch
        if (!DateTime.TryParse(StartDate, out DateTime start) || !DateTime.TryParse(EndDate, out DateTime end))
        {
            Message = "Skriv datumen som 2026-01-15.";
            return;
        }

        if (end < start)
        {
            Message = "Slutdatum kan inte vara före startdatum.";
            return;
        }

        Course course = new()
        {
            CourseNumber = CourseNumber,
            Title = Title,
            Duration = Duration,
            DurationUnit = DurationUnit,
            StartDate = start,
            EndDate = end,
            IsOnline = IsOnline,
            TeacherName = TeacherName
        };

        try
        {
            // Post lägger till ett nytt dokument i restdb
            await _courseService.Add(course);
            Message = $"Kursen {course.CourseNumber} är sparad.";
            ClearData();
        }
        catch (Exception ex)
        {
            Message = "Kunde inte spara: " + ex.Message;
        }
    }

    // tömmer rutorna så man kan skriva in nästa kurs
    private void ClearData()
    {
        CourseNumber = string.Empty;
        Title = string.Empty;
        Duration = 0;
        DurationUnit = "veckor";
        StartDate = string.Empty;
        EndDate = string.Empty;
        IsOnline = false;
        TeacherName = string.Empty;
    }
}
