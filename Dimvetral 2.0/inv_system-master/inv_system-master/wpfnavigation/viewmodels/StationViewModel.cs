using wpfnavigation.Models;

namespace wpfnavigation.viewmodels;

public class StationViewModel
{
    private readonly Station _station;

    public StationViewModel(Station station)
    {
        _station = station;
    }

    public int Id
    {
        get => _station.Id;
    }

    public int DeliveryNoteId
    {
        get => _station.DeliveryNoteId;
    }

    public String Name
    {
        get => _station.Name;
    }

    public DateTime StartDate
    {
        get => _station.StartDate;
    }

    public DateTime EndDate
    {
        get => _station.EndDate;
    }

    public bool Status
    {
        get => _station.Status;
    }

    public string Remark
    {
        get => _station.Remark;
    }
}