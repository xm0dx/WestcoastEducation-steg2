using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

// samma upplägg som EditProductViewModel i RunStore
public partial class EditTeacherViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

    // personnumret som kom med från listan
    private readonly string _personalNumber = string.Empty;

    // restdb:s _id, det måste med i adressen vid uppdatering
    private string _objectId = string.Empty;

    [ObservableProperty]
    public partial string FirstName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string LastName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string PersonalNumber { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Phone { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Address { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string PostalCode { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string City { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string KnowledgeArea { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Message { get; set; } = string.Empty;

    // bara för förhandsvisningen i editorn, hämtar ingen lärare
    public EditTeacherViewModel()
    {
        _teacherService = new TeacherServices();
        PageTitle = "Uppdatera lärare: ";
    }

    public EditTeacherViewModel(ITeacherService teacherService, string personalNumber)
    {
        _teacherService = teacherService;
        PageTitle = "Uppdatera lärare: " + personalNumber;
        _personalNumber = personalNumber;
        Init();
    }

    private async void Init()
    {
        await GetTeacher();
    }

    // hämtar läraren och lägger värdena i rutorna
    private async Task GetTeacher()
    {
        try
        {
            TeacherRestDb teacher = await _teacherService.Find(_personalNumber);
            _objectId = teacher._id;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            PersonalNumber = teacher.PersonalNumber;
            Phone = teacher.Phone;
            Address = teacher.Address;
            PostalCode = teacher.PostalCode;
            City = teacher.City;
            KnowledgeArea = teacher.KnowledgeArea;
        }
        catch (Exception ex)
        {
            Message = "Kunde inte hämta läraren: " + ex.Message;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        // ett Teacher-objekt utan _id, restdb vill inte ha _id i det vi skickar
        Teacher teacher = new()
        {
            FirstName = FirstName,
            LastName = LastName,
            PersonalNumber = PersonalNumber,
            Phone = Phone,
            Address = Address,
            PostalCode = PostalCode,
            City = City,
            KnowledgeArea = KnowledgeArea
        };

        try
        {
            await _teacherService.Update(teacher, _objectId);
            Message = "Läraren är uppdaterad.";
        }
        catch (Exception ex)
        {
            Message = "Kunde inte spara: " + ex.Message;
        }
    }
}
