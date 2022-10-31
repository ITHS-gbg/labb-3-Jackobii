using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class CreateEditQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    public IRelayCommand NavigateMainMenuCommand { get; }

    public CreateEditQuizViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager));
    }
}