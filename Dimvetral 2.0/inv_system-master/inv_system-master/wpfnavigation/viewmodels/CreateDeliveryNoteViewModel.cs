using System.Windows.Input;
using wpfnavigation.Commands;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Services;
using wpfnavigation.Stores;

namespace wpfnavigation.viewmodels;

public class CreateDeliveryNoteViewModel : BaseViewModel
{
    public ICommand NavigateToHomeViewCommand { get; set; }
    public ICommand CreateDeliveryNoteCommand { get; set; }

    private string _name;

    public CreateDeliveryNoteViewModel(NavigationStore navigationStore, DeliveryNoteRepository repository)
    {
        // NavigateToHomeViewCommand = new NavigateToHomeViewCommand(navigationStore);
        //NavigateToHomeViewCommand = new NavigateCommand(navigationStore, () => new HomeViewModel(navigationStore));
        CreateDeliveryNoteCommand = new CreateDeliveryNoteCommand(this, repository, navigationStore);
        NavigateToHomeViewCommand =
            new NavigateCommand(new NavigationService(navigationStore,
                () => new ListingViewModel(navigationStore, repository)));
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private string _remark;
    public string Remark
    {
        get => _remark;
        set
        {
            _remark = value;
            OnPropertyChanged(nameof(Remark));
        }
    }
}