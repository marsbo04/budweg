using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace dimvetral.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly string _connectionString;
        private string _userId;
        private string _password;
        private string _errorMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public LoginViewModel()
        {
            IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configurationBuilder.GetConnectionString("MyDBConnection") ?? string.Empty;
        }

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool ValidateLogin(string userId, string password)
        {
            // Clear previous error message
            ErrorMessage = string.Empty;

            // check input
            if (string.IsNullOrWhiteSpace(userId))
            {
                ErrorMessage = "Bruger-ID er påkrævet";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Kodeord er påkrævet";
                return false;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // Query to check user credentials.... i hope....
                    // Note: here i assume we have a Users table with UserId and PasswordHash columns?????!!!
                    string query = "SELECT PasswordHash FROM Users WHERE UserId = @UserId";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        
                        object? result = cmd.ExecuteScalar();
                        
                        if (result == null)
                        {
                            ErrorMessage = "Ugyldigt Bruger-ID eller kodeord";
                            return false;
                        }

                        string storedPasswordHash = result.ToString() ?? string.Empty;
                        string inputPasswordHash = HashPassword(password);

                        if (storedPasswordHash == inputPasswordHash)
                        {
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "Ugyldigt Bruger-ID eller kodeord";
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = $"Database fejl: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"En fejl opstod: {ex.Message}";
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}