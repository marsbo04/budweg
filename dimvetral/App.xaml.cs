using System.Configuration;
using System.Data;
using System.Windows;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace dimvetral
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            IConfigurationRoot configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string connectionStringName = configurationBuilder.GetConnectionString("MyDBConnection");

            using (SqlConnection con = new SqlConnection(connectionStringName))
            {
                con.Open();
                Console.WriteLine("Connected!\n");

                // Insert a record
                new SqlCommand("INSERT INTO People (Name, Age) VALUES ('Jane', 25)", con).ExecuteNonQuery();
                Console.WriteLine("Record inserted!\n");

                // Read and display all records
                SqlDataReader reader = new SqlCommand("SELECT Id, Name, Age FROM People", con).ExecuteReader();
                Console.WriteLine("ID | Name | Age");
                Console.WriteLine("---|------|----");
                string cool_string = "";
                while (reader.Read())
                {
                    //MessageBox.Show($"{reader["Id"]}  | {reader["Name"]} | {reader["Age"]}");
                    cool_string += $"{reader["Id"]}  | {reader["Name"]} | {reader["Age"]}\n";
                }
                MessageBox.Show(cool_string);

            }
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            // 

            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }
    }

}
