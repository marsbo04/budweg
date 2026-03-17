using wpfnavigation.viewmodels;

namespace wpfnavigation.Stores;

public class NavigationStore
{
    public event Action CurrentViewModelChanged;

    public BaseViewModel _currentViewModel;

    public BaseViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}