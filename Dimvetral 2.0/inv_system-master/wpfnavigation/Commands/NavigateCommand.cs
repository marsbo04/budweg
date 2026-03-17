using System.Windows.Input;
using wpfnavigation.Services;
using wpfnavigation.Stores;
using wpfnavigation.viewmodels;

namespace wpfnavigation.Commands;

public class NavigateCommand : CommandBase
{
    public readonly NavigationService navigationService;

    public NavigateCommand(NavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
        navigationService.Navigate();
    }
}