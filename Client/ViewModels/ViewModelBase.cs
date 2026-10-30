using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModels;

// basklass för alla ViewModels. ObservableObject ger INotifyPropertyChanged,
// alltså förmågan att säga till vyn när ett värde ändras.
// partial krävs för att verktyget ska få generera kod i samma klass
public partial class ViewModelBase : ObservableObject
{
    // alla sidor får en rubrik via arvet
    [ObservableProperty]
    public partial string PageTitle { get; set; } = "";
}
