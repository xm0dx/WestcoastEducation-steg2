using System;
using Client.Enums;
using Client.Interfaces;
using Client.ViewModels;

namespace Client.Factories;

// factory design pattern: en klass vars enda jobb är att skapa rätt sida.
// MainWindowViewModel säger "Courses" och får en CoursesViewModel tillbaka
public class PageViewFactory : IPageViewFactory
{
    public ViewModelBase LoadPageViewModel(PageViewsEnum page)
    {
        // switch som uttryck: "värde => resultat". _ körs om inget passar
        return page switch
        {
            PageViewsEnum.Home => new HomeViewModel(),
            _ => throw new InvalidOperationException("Sidan finns inte")
        };
    }

    public ViewModelBase LoadPageViewModel(PageViewsEnum page, string id)
    {
        return page switch
        {
            _ => throw new InvalidOperationException("Sidan finns inte")
        };
    }
}
