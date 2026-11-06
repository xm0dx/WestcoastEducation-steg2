using Client.Enums;
using Client.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly PageViewFactory _pageViewFactory;

    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; set; } = new();

    public MainWindowViewModel(PageViewFactory pageViewFactory)
    {
        _pageViewFactory = pageViewFactory;
        GoToHome();
    }

    [RelayCommand]
    private void GoToHome() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.Home);
    [RelayCommand]
    private void GoToCourses() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.Courses);
    [RelayCommand]
    private void GoToAddCourse() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.AddCourse);
    [RelayCommand]
    private void GoToStudents() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.Students);
    [RelayCommand]
    private void GoToAddStudent() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.AddStudent);
    [RelayCommand]
    private void GoToTeachers() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.Teachers);
    [RelayCommand]
    private void GoToAddTeacher() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.AddTeacher);

    // commandet får personnumret från knappen på raden (CommandParameter) och skickar det vidare till fabriken
    [RelayCommand]
    private void GoToEditTeacher(string personalNumber) => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.EditTeacher, personalNumber);
}
