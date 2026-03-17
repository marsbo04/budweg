namespace wpfnavigation.Models;

public class DeliveryNote
{
    // For retrieving from DB
    public DeliveryNote(int id, DateTime startDate, bool status, string name)
    {
        Id = id;
        StartDate = startDate;
        Status = status;
        Name = name;
    }

    // For creating from UI
    public DeliveryNote(string name)
    {
        Name = name;
        Status = false;
        StartDate = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public bool Status { get; set; }
    public string Name { get; set; }
}