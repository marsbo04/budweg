using wpfnavigation.Models;

namespace wpfnavigation.viewmodels;

public class DeliveryNoteViewModel : BaseViewModel
{
    private readonly DeliveryNote _deliveryNote;

    public DeliveryNoteViewModel(DeliveryNote deliveryNote)
    {
        _deliveryNote = deliveryNote;
    }

    public DeliveryNote DeliveryNote => _deliveryNote;

    public int Id
    {
        get => _deliveryNote.Id;
    }

    public string Name
    {
        get => _deliveryNote.Name;
        set
        {
            _deliveryNote.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public bool Status
    {
        get => _deliveryNote.Status;
        set
        {
            _deliveryNote.Status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public DateTime StartDate
    {
        get => _deliveryNote.StartDate;
        set
        {
            _deliveryNote.StartDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }
}