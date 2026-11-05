using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

public partial class TeachersViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

    [ObservableProperty]
    public partial ObservableCollection<Teacher> Teachers { get; set; } = [];

    public TeachersViewModel()
    {
        _teacherService = new TeacherServices();
        PageTitle = "Lärare";
    }

    public TeachersViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        PageTitle = "Lärare";
        Init();
    }

    private async void Init()
    {
        await LoadTeachers();
    }

    private async Task LoadTeachers()
    {
        try
        {
            Teachers = new ObservableCollection<Teacher>(await _teacherService.List());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
