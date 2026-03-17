namespace wpfnavigation.Models;

public class Station
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Status { get; set; }
    public string Remark { get; set; }
    public int DeliveryNoteId { get; set; }

    // BuildingName is missing but is not relevant for use case.

    public Station(int id, int deliveryNoteId, string name, DateTime startDate, DateTime endDate, bool status,
        string remark)
    {
        Id = id;
        DeliveryNoteId = deliveryNoteId;
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        Remark = remark;
    }
}