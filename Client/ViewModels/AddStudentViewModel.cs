using System;
using System.Threading.Tasks;
using Client.Interfaces;
using Client.Models;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

// sidan för att lägga till en studerande
public partial class AddStudentViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;

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
    public partial string Message { get; set; } = string.Empty;

    public AddStudentViewModel()
    {
        _studentService = new StudentServices();
        PageTitle = "Ny studerande";
    }

    public AddStudentViewModel(IStudentService studentService)
    {
        _studentService = studentService;
        PageTitle = "Ny studerande";
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(PersonalNumber))
        {
            Message = "Förnamn, efternamn och personnummer måste fyllas i.";
            return;
        }

        Student student = new()
        {
            FirstName = FirstName,
            LastName = LastName,
            PersonalNumber = PersonalNumber,
            Phone = Phone,
            Address = Address,
            PostalCode = PostalCode,
            City = City
        };

        await _studentService.Add(student);

        ClearData();
    }

    private void ClearData()
    {
        Message = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        PersonalNumber = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        PostalCode = string.Empty;
        City = string.Empty;
    }
}
