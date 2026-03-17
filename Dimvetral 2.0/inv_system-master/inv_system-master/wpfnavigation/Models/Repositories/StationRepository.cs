using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace wpfnavigation.Models.Repositories;

public class StationRepository
{
    private List<Station> _stations;

    public StationRepository()
    {
        _stations = new List<Station>();
    }

    public List<Station> GetAllStations()
    {
        IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        _stations.Clear();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlDataReader reader =
                new SqlCommand("SELECT Id, DeliveryNoteId, Name, StartDate, EndDate, Status, Note FROM StationHistory", connection)
                    .ExecuteReader();

            while (reader.Read())
            {
                _stations.Add(new Station((int)reader["Id"], (int)reader["DeliveryNoteId"], reader["Name"].ToString(),
                    (DateTime)reader["StartDate"], (DateTime)reader["EndDate"], (bool)reader["Status"],
                    reader["Note"].ToString()));
            }
        }

        return _stations;
    }
}