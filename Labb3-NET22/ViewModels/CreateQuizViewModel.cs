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

public class CreateQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;
    private string _title;
    private string _statement;
    private ObservableCollection<string> _answers = new () { string.Empty, string.Empty, string.Empty, string.Empty};
    private int _correctAnswer;
    private Question? _selectedQuestion;

    #region Checkbox Fields

    private bool _checkbox1;
    private bool _checkbox2;
    private bool _checkbox3;
    private bool _checkbox4;

    #endregion

    public string Title
    {
        get => _title;
        set
        {
            SetProperty(ref _title, value);
            SaveQuizCommand.NotifyCanExecuteChanged();
        }
    }
    public string Statement
    {
        get => _statement;
        set
        {
            SetProperty(ref _statement, value);
            CreateQuestionCommand.NotifyCanExecuteChanged();
        }
    }
    public ObservableCollection<string> Answers
    {
        get => _answers;
        set
        {
            SetProperty(ref _answers, value);
            CreateQuestionCommand.NotifyCanExecuteChanged();
        }
    }
    public int CorrectAnswer
    {
        get => _correctAnswer;
        set
        {
            SetProperty(ref _correctAnswer, value);
            switch (value)
            {
                case 0:
                    Checkbox1 = true;
                    break;
                case 1:
                    Checkbox2 = true;
                    break;
                case 2:
                    Checkbox3 = true;
                    break;
                case 3:
                    Checkbox4 = true;
                    break;
                default:
                    Checkbox1 = false;
                    Checkbox2 = false;
                    Checkbox3 = false;
                    Checkbox4 = false;
                    break;
            }
            CreateQuestionCommand.NotifyCanExecuteChanged();
        }
    }

    public Question? SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            SetProperty(ref _selectedQuestion, value);
            if (value != null)
            {
                Statement = value.Statement;
                Answers[0] = value.Answers[0];
                Answers[1] = value.Answers[1];
                Answers[2] = value.Answers[2];
                Answers[3] = value.Answers[3];
                CorrectAnswer = value.CorrectAnswer;
            }
            else
            {
                ClearAllFields();
            }
        }
    }

    public bool IsQuestionSelected
    {
        get
        {
            return SelectedQuestion != null;
        }
    }
    public ObservableCollection<Question> Questions { get; set; } = new ();

    #region Checkbox Props

    public bool Checkbox1
    {
        get { return _checkbox1; }
        set
        {
            if (value)
            {
                SetProperty(ref _checkbox1, value);
                _checkbox2 = false;
                _checkbox3 = false;
                _checkbox4 = false;
                _correctAnswer = 0;
                OnPropertyChanged(nameof(Checkbox2));
                OnPropertyChanged(nameof(Checkbox3));
                OnPropertyChanged(nameof(Checkbox4));
                OnPropertyChanged(nameof(CorrectAnswer));
                CreateQuestionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool Checkbox2
    {
        get { return _checkbox2; }
        set
        {
            if (value)
            {
                SetProperty(ref _checkbox2, value);
                _checkbox1 = false;
                _checkbox3 = false;
                _checkbox4 = false;
                _correctAnswer = 1;
                OnPropertyChanged(nameof(Checkbox1));
                OnPropertyChanged(nameof(Checkbox3));
                OnPropertyChanged(nameof(Checkbox4));
                OnPropertyChanged(nameof(CorrectAnswer));
                CreateQuestionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool Checkbox3
    {
        get { return _checkbox3; }
        set
        {
            if (value)
            {
                SetProperty(ref _checkbox3, value);
                _checkbox1 = false;
                _checkbox2 = false;
                _checkbox4 = false;
                _correctAnswer = 2;
                OnPropertyChanged(nameof(Checkbox1));
                OnPropertyChanged(nameof(Checkbox2));
                OnPropertyChanged(nameof(Checkbox4));
                OnPropertyChanged(nameof(CorrectAnswer));
                CreateQuestionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public bool Checkbox4
    {
        get { return _checkbox4; }
        set
        {
            if (value)
            {
                SetProperty(ref _checkbox4, value);
                _checkbox1 = false;
                _checkbox2 = false;
                _checkbox3 = false;
                _correctAnswer = 3;
                OnPropertyChanged(nameof(Checkbox1));
                OnPropertyChanged(nameof(Checkbox2));
                OnPropertyChanged(nameof(Checkbox3));
                OnPropertyChanged(nameof(CorrectAnswer));
                CreateQuestionCommand.NotifyCanExecuteChanged();
            }
        }
    }

    #endregion


    public IRelayCommand NavigateMainMenuCommand { get; }
    public IRelayCommand CreateQuestionCommand { get; }
    public IRelayCommand DeleteQuestionCommand { get; }
    public IRelayCommand SaveQuizCommand { get; }

    public CreateQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager, _quizManager));

        CreateQuestionCommand = new RelayCommand(CreateNewQuestion, CanCreateNewQuestion);
        DeleteQuestionCommand = new RelayCommand(DeleteSelectedQuestion, CanDeleteSelectedQuestion);
        SaveQuizCommand = new RelayCommand(SaveNewQuiz, CanSaveNewQuiz);

        Answers.CollectionChanged += Answers_CollectionChanged; 
    }

    private void Answers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        CreateQuestionCommand.NotifyCanExecuteChanged();
    }
    private void CreateNewQuestion()
    {
        if (Questions.Contains(SelectedQuestion))
        {
            Questions.Add(new Question(Statement, Answers.ToArray(), CorrectAnswer));
            Questions.Remove(SelectedQuestion);
        }
        else
        {
            Questions.Add(new Question(Statement, Answers.ToArray(), CorrectAnswer));
            SaveQuizCommand.NotifyCanExecuteChanged();
            ClearAllFields();
        }
    }
    public bool CanCreateNewQuestion()
    {
        return !string.IsNullOrEmpty(Statement) &&
               !string.IsNullOrEmpty(Answers[0]) &&
               !string.IsNullOrEmpty(Answers[1]) &&
               !string.IsNullOrEmpty(Answers[2]) &&
               !string.IsNullOrEmpty(Answers[3]) &&
               CorrectAnswer is >= 0 and <= 3;
    }
    private void DeleteSelectedQuestion()
    {
        Questions.Remove(SelectedQuestion);
    }
    public bool CanDeleteSelectedQuestion()
    {
        return IsQuestionSelected;
    }
    public void SaveNewQuiz()
    {
        _quizManager.SaveToFileQuiz(new Quiz(Title, Questions));
        _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager, _quizManager);
    }
    private bool CanSaveNewQuiz()
    {
        return Title != null && Questions.Count > 0;
    }
    private void ClearAllFields()
    {
        Statement = string.Empty;
        Answers[0] = string.Empty;
        Answers[1] = string.Empty;
        Answers[2] = string.Empty;
        Answers[3] = string.Empty;
        CorrectAnswer = 0;
        //_checkbox1 = false;
        //_checkbox2 = false;
        //_checkbox3 = false;
        //_checkbox4 = false;
        OnPropertyChanged(nameof(Checkbox1));
        OnPropertyChanged(nameof(Checkbox2));
        OnPropertyChanged(nameof(Checkbox3));
        OnPropertyChanged(nameof(Checkbox4));
    }
}