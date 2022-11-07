﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class ScoreboardViewModel : ObservableObject
{

    private NavigationManager _navigationManager;
    private QuizManager _quizManager;

    private int _score;
    private int _quizLength;

    public int Score => _score;
    public int QuizLength => _quizLength;

    public IRelayCommand NavigateChooseQuizCommand { get; }
    public IRelayCommand NavigateMainMenuCommand { get; }
    public ScoreboardViewModel(NavigationManager navigationManager, QuizManager quizManager, int score, int quizLength)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        NavigateChooseQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager, new QuizManager()));
        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager, _quizManager));

        _score = score;
        _quizLength = quizLength;
    }
}