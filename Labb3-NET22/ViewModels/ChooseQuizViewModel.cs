using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class ChooseQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;
    private Quiz _selectedQuiz;
    public IEnumerable<Quiz> AllQuizzes => _quizManager.AllQuizzes;
    public Quiz SelectedQuiz
    {
        get => _selectedQuiz;
        set => SetProperty(ref _selectedQuiz, value);
    }

    public IRelayCommand NavigatePlayQuizCommand { get; }
    public IRelayCommand NavigateEditQuizCommand { get; }
    public IRelayCommand NavigateMainMenuCommand { get; }
    public IRelayCommand DeleteQuizCommand { get; }
    
    public ChooseQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        NavigatePlayQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new PlayQuizViewModel(_navigationManager, _quizManager, PlaySelectedQuiz()));
        NavigateEditQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager));
        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager, _quizManager));
        DeleteQuizCommand = new RelayCommand(DeleteSelectedQuizCommand);
    }

    public Quiz PlaySelectedQuiz()
    {
        return new Quiz(SelectedQuiz.Title, SelectedQuiz.Questions);
    }
    public void DeleteSelectedQuizCommand()
    {
        _quizManager.RemoveQuiz(SelectedQuiz);
    }
}