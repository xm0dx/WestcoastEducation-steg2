using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

// sidan för att lägga till en lärare. samma som studerande plus kompetensområde
public partial class AddTeacherViewModel : ViewModelBase
{
    private readonly ITeacherService _teacherService;

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

    public AddTeacherViewModel()
    {
        _teacherService = new TeacherServices();
        PageTitle = "Ny lärare";
    }

    public AddTeacherViewModel(ITeacherService teacherService)
    {
        _teacherService = teacherService;
        PageTitle = "Ny lärare";
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(PersonalNumber))
        {
            Message = "Förnamn, efternamn och personnummer måste fyllas i.";
            return;
        }

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
            await _teacherService.Add(teacher);
            Message = $"{teacher.FirstName} {teacher.LastName} är sparad.";
            ClearData();
        }
        catch (Exception ex)
        {
            Message = "Kunde inte spara: " + ex.Message;
        }
    }

    private void ClearData()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        PersonalNumber = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        PostalCode = string.Empty;
        City = string.Empty;
        KnowledgeArea = string.Empty;
    }
}
