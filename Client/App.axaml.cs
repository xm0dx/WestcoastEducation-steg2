using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Helpers;
using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // ServiceCollection är en lista över klasserna appen behöver och hur de skapas
        var collection = new ServiceCollection();
        collection.AddServices();

        var serviceProvider = collection.BuildServiceProvider();
        var vm = serviceProvider.GetRequiredService<MainWindowViewModel>();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                // kopplar ViewModeln till fönstret, alla {Binding} i MainWindow.axaml läser härifrån
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
