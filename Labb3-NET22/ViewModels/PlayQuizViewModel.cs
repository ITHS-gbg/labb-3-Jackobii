using CommunityToolkit.Mvvm.ComponentModel;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class PlayQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;

    public PlayQuizViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }
}
