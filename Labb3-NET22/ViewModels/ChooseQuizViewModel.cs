﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class ChooseQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    public IRelayCommand NavigatePlayQuizCommand { get; }
    public IRelayCommand NavigateEditQuizCommand { get; }
    public IRelayCommand NavigateMainMenuCommand { get; }
    public ChooseQuizViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        NavigatePlayQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new PlayQuizViewModel(_navigationManager));
        NavigateEditQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager));
        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager));
    }
}