using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using dimvetral.Models;
using dimvetral.Models.Repo;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace dimvetral.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly string _connectionString;
        private readonly RepoTrackingSlip _repository;
        private string _employeeId;
        private string _caliberId;
        private bool _isEmployeeLoggedIn;
        private bool _isCaliberSelected;
        private ObservableCollection<CaliberTrackingSlip> _trackingHistory;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configurationBuilder.GetConnectionString("MyDBConnection") ?? string.Empty;
            _repository = new RepoTrackingSlip();
            _trackingHistory = new ObservableCollection<CaliberTrackingSlip>();

            // Initialize commands
            AddNewCaliberCommand = new RelayCommand(AddNewCaliber);
            LogoutCommand = new RelayCommand(Logout);

            LoadTrackingHistory();
        }

        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                _isEmployeeLoggedIn = !string.IsNullOrEmpty(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEmployeeLoggedIn));
            }
        }

        public string CaliberId
        {
            get => _caliberId;
            set
            {
                _caliberId = value;
                _isCaliberSelected = !string.IsNullOrEmpty(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsCaliberSelected));
            }
        }

        public bool IsEmployeeLoggedIn
        {
            get => _isEmployeeLoggedIn;
        }

        public bool IsCaliberSelected
        {
            get => _isCaliberSelected;
        }

        public ObservableCollection<CaliberTrackingSlip> TrackingHistory
        {
            get => _trackingHistory;
            set
            {
                _trackingHistory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNewCaliberCommand { get; }
        public ICommand LogoutCommand { get; }

        private void LoadTrackingHistory()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    
                    string query = @"SELECT CaliberTrackingSlipID, CaliberTrackingSlipName, 
                                           History, Status, Warehouse, StartDate 
                                     FROM CaliberTrackingSlips 
                                     ORDER BY StartDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            TrackingHistory.Clear();
                            
                            while (reader.Read())
                            {
                                var trackingSlip = new CaliberTrackingSlip(
                                    reader["CaliberTrackingSlipID"].ToString() ?? string.Empty,
                                    reader["CaliberTrackingSlipName"].ToString() ?? string.Empty,
                                    reader["History"].ToString() ?? string.Empty,
                                    (bool)(reader["Status"] ?? bool.FalseString),
                                    reader["Warehouse"].ToString() ?? string.Empty,
                                    Convert.ToDateTime(reader["StartDate"])
                                );
                                
                                TrackingHistory.Add(trackingSlip);
                                _repository.add(trackingSlip);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af historik: {ex.Message}", 
                              "Database Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddNewCaliber(object? parameter)
        {
            var caliberInputWindow = new Kaliber_IDInputWindow();
            
            if (caliberInputWindow.ShowDialog() == true)
            {
                CaliberId = caliberInputWindow.CaliberId;
                
                // Save to database
                SaveNewCaliber(CaliberId);
                
                // Refresh history
                LoadTrackingHistory();
                
                MessageBox.Show($"Ny kaliber tilføjet: {CaliberId}", 
                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveNewCaliber(string caliberId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    
                    string query = @"INSERT INTO CaliberTrackingSlips 
                                    (CaliberTrackingSlipID, CaliberTrackingSlipName, History, 
                                    Status, Warehouse, StartDate) 
                                    VALUES (@CaliberId, @Name, @History, @Location, @Status, @Warehouse, @StartDate)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CaliberId", caliberId);
                        cmd.Parameters.AddWithValue("@Name", $"Kaliber {caliberId}");
                        cmd.Parameters.AddWithValue("@History", $"Oprettet af medarbejder {EmployeeId} den {DateTime.Now:dd-MM-yyyy HH:mm}");
                        cmd.Parameters.AddWithValue("@Status", "Aktiv");
                        cmd.Parameters.AddWithValue("@Warehouse", "Hovedlager");
                        cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved lagring af kaliber: {ex.Message}", 
                              "Database Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Logout(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show(
                "Er du sikker på, at du vil logge ud?",
                "Log ud",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Clear current session
                EmployeeId = string.Empty;
                CaliberId = string.Empty;
                
                // Open login window and close current window
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                
                Application.Current.MainWindow?.Close();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand implementation
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}