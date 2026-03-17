using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace wpfnavigation.Models.Repositories;

public class DeliveryNoteRepository
{
    private List<DeliveryNote> _deliveryNotes;

    public DeliveryNoteRepository()
    {
        _deliveryNotes = new List<DeliveryNote>();
    }

    public void LoadDeliveryNotes()
    {
        IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        _deliveryNotes.Clear();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlDataReader reader = new SqlCommand("SELECT Id, StartDate, Status, Name FROM DeliveryNote", connection)
                .ExecuteReader();

            while (reader.Read())
            {
                _deliveryNotes.Add(new DeliveryNote(
                    reader.GetInt32(0),
                    reader.GetDateTime(1),
                    reader.GetBoolean(2),
                    reader.GetString(3)
                ));
            }
        }
    }

    public List<DeliveryNote> GetAllDeliveryNotes()
    {
        return _deliveryNotes;
    }

    public void AddDeliveryNote(DeliveryNote deliveryNote)
    {
        IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand(
                "INSERT INTO DeliveryNote (StartDate, Status, Name) VALUES (@StartDate, @Status, @Name); SELECT SCOPE_IDENTITY();",
                connection);
            command.Parameters.AddWithValue("@StartDate", deliveryNote.StartDate);
            command.Parameters.AddWithValue("@Status", deliveryNote.Status);
            command.Parameters.AddWithValue("@Name", deliveryNote.Name);

            int newId = Convert.ToInt32(command.ExecuteScalar());
            deliveryNote.Id = newId;
            _deliveryNotes.Add(deliveryNote);
        }
    }

    public void UpdateDeliveryNote(DeliveryNote deliveryNote)
    {
        IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand command = new SqlCommand(
                "UPDATE DeliveryNote SET StartDate = @StartDate, Status = @Status, Name = @Name WHERE Id = @Id",
                connection);
            command.Parameters.AddWithValue("@StartDate", deliveryNote.StartDate);
            command.Parameters.AddWithValue("@Status", deliveryNote.Status);
            command.Parameters.AddWithValue("@Name", deliveryNote.Name);
            command.Parameters.AddWithValue("@Id", deliveryNote.Id);

            command.ExecuteNonQuery();
        }
    }
}