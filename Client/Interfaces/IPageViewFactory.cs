using Client.Enums;
using Client.ViewModels;

namespace Client.Interfaces;

// löftet: en sidfabrik kan ge en ViewModel för ett sidnamn
public interface IPageViewFactory
{
    ViewModelBase LoadPageViewModel(PageViewsEnum page);
    // samma namn men med ett id också, för sidor som ska visa ett visst objekt
    ViewModelBase LoadPageViewModel(PageViewsEnum page, string id);
}
