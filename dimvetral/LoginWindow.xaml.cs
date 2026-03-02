using dimvetral.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dimvetral
{
    /// Interaction logic for LoginWindow.xaml

    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            DataContext = _viewModel;
            
            UpdatePasswordPlaceholder();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            PerformLogin();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter key to submit login
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            string userId = UserIdTextBox.Text;
            string password = PasswordBox.Password;

            // Remove placeholder text
            if (userId == "Indtast Bruger-ID...")
            {
                userId = string.Empty;
            }

            bool isValid = _viewModel.ValidateLogin(userId, password);

            if (isValid)
            {
              // Login successful -> open main window with employee ID
                MainWindow mainWindow = new MainWindow(userId);
                mainWindow.Show();
                this.Close();
            }
            else
            {
             //Login not successful -> Show error message
                ErrorMessageTextBlock.Text = _viewModel.ErrorMessage;
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                
              // Clear password
                PasswordBox.Clear();
            }
        }

        private void UserIdTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
         // Clear placeholder text
            if (UserIdTextBox.Text == "Indtast Bruger-ID...")
            {
                UserIdTextBox.Text = string.Empty;
                UserIdTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
            
         // Hide error message when user starts typing
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void UserIdTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
         // Restore placeholder text
            if (string.IsNullOrWhiteSpace(UserIdTextBox.Text))
            {
                UserIdTextBox.Text = "Indtast Bruger-ID...";
                UserIdTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholder();
            
        // Hide error message when user starts typing
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void UpdatePasswordPlaceholder()
        {
        // Hide placeholder when password box has content
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password) 
                ? Visibility.Visible 
                : Visibility.Collapsed;
            
         // Update placeholder visibility when password changes
            PasswordBox.PasswordChanged += (s, e) =>
            {
                PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            };
        }
    }
}
