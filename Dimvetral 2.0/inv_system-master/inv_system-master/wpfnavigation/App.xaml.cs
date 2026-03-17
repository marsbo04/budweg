using System.Configuration;
using System.Data;
using System.Windows;
using wpfnavigation.Models.Repositories;
using wpfnavigation.Stores;
using wpfnavigation.viewmodels;

namespace wpfnavigation;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        /*IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        string connectionString = configurationBuilder.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlDataReader reader = new SqlCommand("SELECT * FROM People", connection).ExecuteReader();

            string cool_string = "";
            while (reader.Read())
            {
                cool_string += $"{reader["first_name"]} / {reader["phone"]}\n";
            }

            MessageBox.Show(cool_string);
        }*/

        DeliveryNoteRepository  repository = new DeliveryNoteRepository();
        repository.LoadDeliveryNotes();

        NavigationStore navigationStore = new NavigationStore();
        navigationStore.CurrentViewModel = new ListingViewModel(navigationStore, repository);
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(navigationStore)
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}