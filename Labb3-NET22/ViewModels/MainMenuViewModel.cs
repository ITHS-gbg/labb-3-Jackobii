using System.CodeDom;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class MainMenuViewModel : ObservableObject
{
    private NavigationManager _navigationManager;

    public MainMenuViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }
}