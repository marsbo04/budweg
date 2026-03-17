using wpfnavigation.Models;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Stores;
using wpfnavigation.viewmodels;

namespace wpfnavigation.Commands;

public class CreateDeliveryNoteCommand : CommandBase
{
    private readonly CreateDeliveryNoteViewModel _createCreateDeliveryNoteViewModelViewModel;
    private readonly DeliveryNoteRepository _repository;
    private readonly NavigationStore _navigationStore;

    public CreateDeliveryNoteCommand(CreateDeliveryNoteViewModel createDeliveryNoteViewModel, DeliveryNoteRepository repository,
        NavigationStore navigationStore)
    {
        _createCreateDeliveryNoteViewModelViewModel = createDeliveryNoteViewModel;
        _repository = repository;
        _navigationStore = navigationStore;
    }

    public override void Execute(object parameter)
    {
        var newDeliveryNote = new DeliveryNote(_createCreateDeliveryNoteViewModelViewModel.Name);
        _repository.AddDeliveryNote(newDeliveryNote); 
    }
}