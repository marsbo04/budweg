using System.Windows.Input;
using wpfnavigation.Commands;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Services;
using wpfnavigation.Stores;

namespace wpfnavigation.viewmodels;

public class SecondViewModel : BaseViewModel
{
    public ICommand NavigateToHomeViewCommand2 { get; set; }

    public SecondViewModel(NavigationStore navigationStore, DeliveryNoteRepository deliveryNoteRepository)
    {
        // NavigateToHomeViewCommand2 = new NavigateToHomeViewCommand(navigationStore);
        // NavigateToHomeViewCommand2 = new NavigateCommand(navigationStore, () => new HomeViewModel(navigationStore));
        NavigateToHomeViewCommand2 =
            new NavigateCommand(new NavigationService(navigationStore,
                () => new ListingViewModel(navigationStore, deliveryNoteRepository)));
    }
}