using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using wpfnavigation.Commands;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Services;
using wpfnavigation.Stores;

namespace wpfnavigation.viewmodels;

public class SecondViewModel : BaseViewModel
{
    public ICommand NavigateToHomeViewCommand2 { get; set; }
    private StationRepository _stationRepository;
    private readonly ObservableCollection<StationViewModel> _stations;
    public ObservableCollection<StationViewModel> Stations => _stations;

    public SecondViewModel(NavigationStore navigationStore, DeliveryNoteRepository deliveryNoteRepository)
    {
        // NavigateToHomeViewCommand2 = new NavigateToHomeViewCommand(navigationStore);
        // NavigateToHomeViewCommand2 = new NavigateCommand(navigationStore, () => new HomeViewModel(navigationStore));
        NavigateToHomeViewCommand2 =
            new NavigateCommand(new NavigationService(navigationStore,
                () => new ListingViewModel(navigationStore, deliveryNoteRepository)));

        // The repo is initialized here because stations are only view-only at this point in the program.
        _stationRepository = new StationRepository();
        _stations = new ObservableCollection<StationViewModel>();

        Refresh();
    }

    private void Refresh()
    {
        _stations.Clear(); 
        foreach (var station in _stationRepository.GetAllStations())
        {
            StationViewModel viewModel = new StationViewModel(station);
            _stations.Add(viewModel);
        }
    }
}