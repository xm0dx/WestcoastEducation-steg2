using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

// sidan för att hantera en lärares kompetensområde.
// flödet: knappen på raden skickar _id -> MainWindowViewModel.GoToEditTeacher -> fabriken
// -> new EditTeacherViewModel(tjänst, id) -> Init hämtar läraren -> rutorna fylls i
public partial class EditTeacherViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

    // restdb:s _id, måste med i adressen när vi uppdaterar
    private readonly string _id = string.Empty;

    // läraren som hämtades, vi ändrar bara kompetensområdet och skickar tillbaka resten som det var
    private TeacherRestDb? _teacher;

    [ObservableProperty]
    public partial string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string KnowledgeArea { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;

    // bara för förhandsvisningen i editorn
    public EditTeacherViewModel()
    {
        _teacherService = new TeacherServices();
        PageTitle = "Kompetensområde";
    }

    public EditTeacherViewModel(ITeacherService teacherService, string id)
    {
        _teacherService = teacherService;
        _id = id;
        PageTitle = "Kompetensområde";
        Init();
    }

    private async void Init()
    {
        try
        {
            _teacher = await _teacherService.Find(_id);
            Name = $"{_teacher.FirstName} {_teacher.LastName}";
            KnowledgeArea = _teacher.KnowledgeArea;
        }
        catch (Exception ex)
        {
            Message = "Kunde inte hämta läraren: " + ex.Message;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (_teacher is null) return;

        // ett nytt Teacher-objekt utan _id, restdb vill inte ha _id i det vi skickar
        Teacher teacher = new()
        {
            FirstName = _teacher.FirstName,
            LastName = _teacher.LastName,
            PersonalNumber = _teacher.PersonalNumber,
            Phone = _teacher.Phone,
            Address = _teacher.Address,
            PostalCode = _teacher.PostalCode,
            City = _teacher.City,
            KnowledgeArea = KnowledgeArea
        };

        try
        {
            await _teacherService.Update(teacher, _id);
            Message = "Kompetensområdet är sparat.";
        }
        catch (Exception ex)
        {
            Message = "Kunde inte spara: " + ex.Message;
        }
    }
}
