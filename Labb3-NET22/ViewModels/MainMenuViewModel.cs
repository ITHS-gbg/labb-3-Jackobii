using System.CodeDom;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class MainMenuViewModel : ObservableObject
{
    private NavigationManager _navigationManager;

    public IRelayCommand NavigateChooseQuizCommand { get; }
    public IRelayCommand NavigateCreateEditQuizCommand { get; }
    public IRelayCommand NavigateEndProgramCommand { get; }

    public MainMenuViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        NavigateChooseQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager));
        NavigateCreateEditQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new CreateEditQuizViewModel(_navigationManager));
        NavigateEndProgramCommand = new RelayCommand(() => System.Environment.Exit(0));
    }
}