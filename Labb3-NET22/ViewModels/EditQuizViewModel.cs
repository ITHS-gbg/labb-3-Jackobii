using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class EditQuizViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private QuizManager _quizManager;
    public IRelayCommand NavigateMainMenuCommand { get; }

    public EditQuizViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        NavigateMainMenuCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new MainMenuViewModel(_navigationManager, _quizManager));
    }
}