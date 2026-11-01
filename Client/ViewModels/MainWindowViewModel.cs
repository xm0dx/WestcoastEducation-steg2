using Client.Enums;
using Client.Factories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    // fabriken skapar sidan när man klickar, fönstret vet inte vilka sidor som finns
    private readonly PageViewFactory _pageViewFactory;

    // sidan som visas. MainWindow.axaml visar den via <UserControl Content="{Binding CurrentView}"/>
    [ObservableProperty]
    public partial ViewModelBase CurrentView { get; set; } = new();

    public MainWindowViewModel(PageViewFactory pageViewFactory)
    {
        _pageViewFactory = pageViewFactory;
        GoToHome();
    }

    // varje knapp i menyn = ett namn in i fabriken, en ny sida ut
    [RelayCommand]
    private void GoToHome() => CurrentView = _pageViewFactory.LoadPageViewModel(PageViewsEnum.Home);
}
