using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class StudentsViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;

    [ObservableProperty]
    public partial ObservableCollection<Student> Students { get; set; } = [];

    public StudentsViewModel()
    {
        _studentService = new StudentServices();
        PageTitle = "Studerande";
    }

    public StudentsViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        PageTitle = "Studerande";
        Init();
    }

    private async void Init()
    {
        await LoadStudents();
    }

    private async Task LoadStudents()
    {
        try
        {
            Students = new ObservableCollection<Student>(await _studentService.List());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
