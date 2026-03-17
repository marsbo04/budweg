using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;

namespace wpfnavigation.Models.Repositories;

public class DeliveryNoteRepository
{
    private List<DeliveryNote> _deliveryNotes;
    private readonly string _connectionString;

    public DeliveryNoteRepository()
    {
        _deliveryNotes = new List<DeliveryNote>();
        
        IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        // Ensure database and schema exists
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        string databaseName = builder.InitialCatalog;
        
        // Temporarily point to the 'master' database to create our target database
        builder.InitialCatalog = "master";

        using (SqlConnection masterConnection = new SqlConnection(builder.ConnectionString))
        {
            masterConnection.Open();
            
            // Check if our database exists, create it if it doesn't
            using (SqlCommand checkDbCmd = new SqlCommand($"SELECT db_id('{databaseName}')", masterConnection))
            {
                var result = checkDbCmd.ExecuteScalar();
                if (result == DBNull.Value || result == null)
                {
                    using (SqlCommand createDbCmd = new SqlCommand($"CREATE DATABASE [{databaseName}]", masterConnection))
                    {
                        createDbCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Now connect to the actual database to ensure tables exist
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string createTableSql = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryNote' and xtype='U')
                BEGIN
                    CREATE TABLE DeliveryNote (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        StartDate DATETIME2 NOT NULL,
                        Status BIT NOT NULL,
                        Name NVARCHAR(255) NOT NULL
                    )
                END";

            using (SqlCommand createTableCmd = new SqlCommand(createTableSql, connection))
            {
                createTableCmd.ExecuteNonQuery();
            }
        }
    }

    public void LoadDeliveryNotes()
    {
        _deliveryNotes.Clear();

        using (SqlConnection connection = new SqlConnection(_connectionString))
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
        using (SqlConnection connection = new SqlConnection(_connectionString))
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
        using (SqlConnection connection = new SqlConnection(_connectionString))
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