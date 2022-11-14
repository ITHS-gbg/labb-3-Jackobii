using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class ChooseQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;
    private Quiz? _selectedQuiz;
    private int? _selectedCategoryIndex;
    private int _selectedTab;
    public ObservableCollection<Quiz> AllQuizzes => (ObservableCollection<Quiz>)_quizManager.AllQuizzes;
    public string[] CategoryQuizzes => Enum.GetNames(typeof(Category));

    public int? SelectedCategoryIndex
    {
        get
        {
            return _selectedCategoryIndex;
        }
        set
        {
            SetProperty(ref _selectedCategoryIndex, value);
            NavigatePlayQuizCommand.NotifyCanExecuteChanged();
            NullSelectedQuiz();
        }
    }

    public int SelectedTab
    {
        get
        {
            return _selectedTab;
        }
        set
        {
            SetProperty(ref _selectedTab, value);
            SelectedQuiz = null;
            DeleteQuizCommand.NotifyCanExecuteChanged();
            NavigateEditQuizCommand.NotifyCanExecuteChanged();
        }
    }
    public Quiz? SelectedQuiz
    {
        get 
        {
            return _selectedQuiz;
        }
        set 
        {
            SetProperty(ref _selectedQuiz, value);
            DeleteQuizCommand.NotifyCanExecuteChanged(); 
            NavigateEditQuizCommand.NotifyCanExecuteChanged();
            NavigatePlayQuizCommand.NotifyCanExecuteChanged();
        }
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
            _navigationManager.CurrentViewModel = new PlayQuizViewModel(_navigationManager, _quizManager, PlaySelectedQuiz()), IsCategoryOrQuizSelected);
        NavigateEditQuizCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new EditQuizViewModel(_navigationManager, _quizManager, SelectedQuiz), IsQuizSelectedCommand);
        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager, _quizManager));
        DeleteQuizCommand = new RelayCommand(DeleteSelectedQuizCommand, IsQuizSelectedCommand);
    }

    public Quiz PlaySelectedQuiz()
    {
        if(SelectedTab == 0 && SelectedQuiz != null)
        {
            return new Quiz(SelectedQuiz.Title, SelectedQuiz.Questions);
        }
        else 
        {
            Quiz catQuiz = _quizManager.GenerateRandomQuiz((int)SelectedCategoryIndex);
            if (catQuiz.Questions.Count() > 0)
            {
                return catQuiz;
            }
            else
            {
                _navigationManager.CurrentViewModel = new ScoreboardViewModel(_navigationManager, _quizManager, 0, 0);
                return catQuiz;
            }
        }
    }
    public void DeleteSelectedQuizCommand()
    {
        _quizManager.DeleteQuiz(SelectedQuiz);
    }
    public bool IsQuizSelectedCommand()
    {
        return SelectedQuiz != null;
    }

    public bool IsCategoryOrQuizSelected()
    {
        return SelectedQuiz != null || SelectedCategoryIndex != null;
    }
    public void NullSelectedQuiz()
    {
        SelectedQuiz = null;
        DeleteQuizCommand.NotifyCanExecuteChanged();
        NavigateEditQuizCommand.NotifyCanExecuteChanged();
    }
}