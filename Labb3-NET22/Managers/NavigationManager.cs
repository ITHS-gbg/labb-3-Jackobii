using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Labb3_NET22.Managers;

public class NavigationManager : ObservableObject
{

    private ObservableObject _currentViewModel;

    public ObservableObject CurrentViewModel
    {
        get { return _currentViewModel; }
        set 
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged() 
    { 
        CurrentViewModelChanged?.Invoke();
    }

    public event Action CurrentViewModelChanged;
}