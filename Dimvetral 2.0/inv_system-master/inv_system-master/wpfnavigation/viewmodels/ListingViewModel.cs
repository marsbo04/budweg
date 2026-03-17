using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using wpfnavigation.Commands;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Stores;

namespace wpfnavigation.viewmodels;

public class ListingViewModel : BaseViewModel
{
    public ICommand NavigateToFirstViewCommand { get; }
    public ICommand NavigateToSecondViewCommand { get; }
    private readonly ObservableCollection<DeliveryNoteViewModel> _deliveryNotes;
    public ObservableCollection<DeliveryNoteViewModel> DeliveryNotes => _deliveryNotes;

    private readonly DeliveryNoteRepository _deliveryNoteRepository;

    public ListingViewModel(NavigationStore navigationStore, DeliveryNoteRepository deliveryNoteRepository)
    {
        NavigateToFirstViewCommand =
            new NavigateCommand(new Services.NavigationService(navigationStore,
                () => new CreateDeliveryNoteViewModel(navigationStore, deliveryNoteRepository)));
        NavigateToSecondViewCommand =
            new NavigateCommand(new Services.NavigationService(navigationStore,
                () => new SecondViewModel(navigationStore, deliveryNoteRepository)));

        _deliveryNotes = new ObservableCollection<DeliveryNoteViewModel>();
        this._deliveryNoteRepository = deliveryNoteRepository;

        Refresh();
    }

    private void Refresh()
    {
        // This should probably reload from database
        _deliveryNotes.Clear();
        foreach (var note in _deliveryNoteRepository.GetAllDeliveryNotes())
        {
            DeliveryNoteViewModel viewModel = new DeliveryNoteViewModel(note);
            // Since _deliveryNotes is an ObservableCollection, the UI will update when we add items to it. We need to listen for changes on the individual view models so the database can be updated when they change.
            viewModel.PropertyChanged += OnDeliveryNoteChanged;
            _deliveryNotes.Add(viewModel);
        }
    }

    private void OnDeliveryNoteChanged(object? sender, PropertyChangedEventArgs e)
    {
        var deliveryNoteViewModel = sender as DeliveryNoteViewModel;
        if (deliveryNoteViewModel == null)
        {
            return;
        }

        if (e.PropertyName == nameof(DeliveryNoteViewModel.Status))
        {
            _deliveryNoteRepository.UpdateDeliveryNote(deliveryNoteViewModel.DeliveryNote);
        }
    }
}