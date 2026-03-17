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

    private readonly DeliveryNoteRepository deliveryNoteRepository;

    public ListingViewModel(NavigationStore navigationStore, DeliveryNoteRepository deliveryNoteRepository)
    {
        NavigateToFirstViewCommand =
            new NavigateCommand(new Services.NavigationService(navigationStore,
                () => new CreateDeliveryNoteViewModel(navigationStore, deliveryNoteRepository)));
        NavigateToSecondViewCommand =
            new NavigateCommand(new Services.NavigationService(navigationStore,
                () => new SecondViewModel(navigationStore, deliveryNoteRepository)));

        _deliveryNotes = new ObservableCollection<DeliveryNoteViewModel>();
        this.deliveryNoteRepository = deliveryNoteRepository;

        Refresh();
    }

    private void Refresh()
    {
        // This should probably reload from database
        _deliveryNotes.Clear();
        foreach (var note in deliveryNoteRepository.GetAllDeliveryNotes())
        {
            DeliveryNoteViewModel viewModel = new DeliveryNoteViewModel(note);
            viewModel.PropertyChanged += OnDeliveryNoteChanged;
            _deliveryNotes.Add(viewModel);
        }
    }

    private void OnDeliveryNoteChanged(object? sender, PropertyChangedEventArgs e)
    {
        var noteViewModel = sender as DeliveryNoteViewModel;
        if (noteViewModel == null)
        {
            return;
        }

        if (e.PropertyName == nameof(DeliveryNoteViewModel.Status))
        {
            deliveryNoteRepository.UpdateDeliveryNote(noteViewModel.DeliveryNote);
        }
    }
}