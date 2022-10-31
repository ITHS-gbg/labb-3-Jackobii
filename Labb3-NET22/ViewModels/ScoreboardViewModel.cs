using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class ScoreboardViewModel : ObservableObject
{

    private NavigationManager _navigationManager;

    public IRelayCommand NavigateChooseQuizCommand { get; }
    public IRelayCommand NavigateMainMenuCommand { get; }
    public ScoreboardViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        NavigateChooseQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager));
        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager));
    }
}