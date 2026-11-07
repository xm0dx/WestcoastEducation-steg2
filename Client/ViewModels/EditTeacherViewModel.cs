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

    private readonly string _personalNumber = string.Empty;

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

    private async Task GetTeacher()
    {
        var teacher = await _teacherService.Find(_personalNumber);

        _objectId = teacher._id;

        FirstName = teacher.FirstName;
        LastName = teacher.LastName;
        PersonalNumber = teacher.PersonalNumber;
        Phone = teacher.Phone ?? string.Empty;
        Address = teacher.Address ?? string.Empty;
        PostalCode = teacher.PostalCode ?? string.Empty;
        City = teacher.City ?? string.Empty;
        KnowledgeArea = teacher.KnowledgeArea ?? string.Empty;
    }

    [RelayCommand]
    private async Task Save()
    {
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

        await _teacherService.Update(teacher, _objectId);
    }
}
