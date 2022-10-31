using CommunityToolkit.Mvvm.ComponentModel;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    private readonly NavigationManager _navigationManager;

    public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

    public MainWindowViewModel(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;

        _navigationManager.CurrentViewModelChanged += CurrentViewModelChanged;
    }

    private void CurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}