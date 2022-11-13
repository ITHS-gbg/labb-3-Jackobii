using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class PlayQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;
    private readonly Quiz _quiz;
    private int _quizLength;
    private int _correctAnswer;
    private Question _currentQuestion;
    private int _questionsAnswered = 0;
    private int _score = 0;
    private string _pictureFilePath;
    private bool _questionGotPicture;

    public int QuestionsAnswered {
        get => _questionsAnswered;
        set { _questionsAnswered = value; OnPropertyChanged(nameof(PossibleScore)); }
    }

    public int Score
    {
        get { return _score; }
        set { _score = value; OnPropertyChanged(nameof(UserScore)); }
    }
    public string UserScore
    {
        get { return $"Din poäng: {Score}"; }
    }

    public string PossibleScore
    {
        get { return $"Besvarade frågor: {QuestionsAnswered}"; }
    }

    public Question CurrentQuestion
    {
        get { return _currentQuestion; }
        set
        {
            SetProperty(ref _currentQuestion, value);
        }
    }

    public int CorrectAnswer
    {
        get { return _correctAnswer; }
        set
        {
            SetProperty(ref _correctAnswer, value);
        }
    }

    public string PictureFilePath
    {
        get { return _pictureFilePath; }
        set
        {
            SetProperty(ref _pictureFilePath, value);
        }
    }

    public bool QuestionGotPicture
    {
        get { return _questionGotPicture; }
        set { SetProperty(ref _questionGotPicture, value); }
    }


    public IRelayCommand AnswerOneCommand { get; }
    public IRelayCommand AnswerTwoCommand { get; }
    public IRelayCommand AnswerThreeCommand { get; }
    public IRelayCommand AnswerFourCommand { get; }
    public IRelayCommand NavigateScoreboardCommand { get; }

    public PlayQuizViewModel(NavigationManager navigationManager, QuizManager quizManager, Quiz quiz)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;
        _quiz = quiz;

        AnswerOneCommand = new RelayCommand(AnswerOne);
        AnswerTwoCommand = new RelayCommand(AnswerTwo);
        AnswerThreeCommand = new RelayCommand(AnswerThree);
        AnswerFourCommand = new RelayCommand(AnswerFour);

        NavigateScoreboardCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new ScoreboardViewModel(_navigationManager, _quizManager, Score, _quizLength));


        _quizLength = quiz.Questions.Count();

        DisplayNewQuestion();
    }

    private void AnswerOne()
    {
        if (CurrentQuestion.CorrectAnswer == 0)
        {
            Score++;
            OnPropertyChanged(nameof(Score));
        }
        UpdateQuestionsAnswered();
        DisplayNewQuestion();
    }

    private void AnswerTwo()
    {
        if (CurrentQuestion.CorrectAnswer == 1)
        {
            Score++;
            OnPropertyChanged(nameof(Score));
        }
        UpdateQuestionsAnswered();
        DisplayNewQuestion();
    }

    private void AnswerThree()
    {
        if (CurrentQuestion.CorrectAnswer == 2)
        {
            Score++;
            OnPropertyChanged(nameof(Score));
        }
        UpdateQuestionsAnswered();
        DisplayNewQuestion();
    }

    private void AnswerFour()
    {
        if (CurrentQuestion.CorrectAnswer == 3)
        {
            Score++;
            OnPropertyChanged(nameof(Score));
        }
        UpdateQuestionsAnswered();
        DisplayNewQuestion();
    }

    private async Task DisplayNewQuestion()
    {
        if (!_quiz.Questions.Any())
        {
            await Task.Delay(500);
            _navigationManager.CurrentViewModel =
                new ScoreboardViewModel(_navigationManager, _quizManager, Score, QuestionsAnswered);
            return;
        }

        CurrentQuestion = _quiz.GetRandomQuestion();
        CorrectAnswer = CurrentQuestion.CorrectAnswer;
        UpdatePictureFilePath();
    }

    private void UpdateQuestionsAnswered()
    {
        QuestionsAnswered++;
        OnPropertyChanged(nameof(QuestionsAnswered));
    }

    private void UpdatePictureFilePath()
    {
        if (CurrentQuestion.QuestionPicturePath != _quizManager.NoImagePath || CurrentQuestion.QuestionPicturePath != string.Empty)
        {
            PictureFilePath = CurrentQuestion.QuestionPicturePath;
            QuestionGotPicture = true;
        }
        else
        {
            PictureFilePath = string.Empty;
            QuestionGotPicture = false;
        }
    }
}
