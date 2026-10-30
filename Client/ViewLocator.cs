using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Client.ViewModels;

namespace Client;

// hittar rätt View till en ViewModel via namnet.
// Client.ViewModels.CoursesViewModel -> Client.Views.CoursesView
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null) return null;

        // byt "ViewModel" mot "View" i hela namnet
        var viewName = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var view = Type.GetType(viewName);

        if (view is null) return null;

        // skapa vyn och koppla den till sin ViewModel
        var control = (Control)Activator.CreateInstance(view)!;
        control.DataContext = data;

        return control;
    }

    // ViewLocator används bara för våra egna ViewModels
    public bool Match(object? data) => data is ViewModelBase;
}
