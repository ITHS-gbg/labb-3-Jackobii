using System.CodeDom;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class MainMenuViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;

    public IRelayCommand NavigateChooseQuizCommand { get; }
    public IRelayCommand NavigateCreateQuizCommand { get; }
    public IRelayCommand NavigateEndProgramCommand { get; }

    public MainMenuViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        NavigateChooseQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager, _quizManager));
        NavigateCreateQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new CreateQuizViewModel(_navigationManager, _quizManager));
        NavigateEndProgramCommand = new RelayCommand(() => System.Environment.Exit(0));
    }
}