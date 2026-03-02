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

    /// Interaction logic for Kaliber_IDInputWindow.xaml
 
    public partial class Kaliber_IDInputWindow : Window
    {
        public string CaliberId { get; private set; }

        public Kaliber_IDInputWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Clear placeholder on first character
                if (textBox.Text == "Indtast Kaliber-ID..." || textBox.Text.StartsWith("Indtast Kaliber-ID..."))
                {
                    // User is starting to type, clear placeholder
                    if (textBox.Text.Length > "Indtast Kaliber-ID...".Length)
                    {
                        textBox.Text = textBox.Text.Replace("Indtast Kaliber-ID...", "");
                        textBox.Foreground = new SolidColorBrush(Colors.Black);
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Clear placeholder text
                if (textBox.Text == "Indtast Kaliber-ID...")
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Restore placeholder text
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = "Indtast Kaliber-ID...";
                    textBox.Foreground = new SolidColorBrush(Colors.Gray);
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                SubmitCaliberId();
            }
        }

        private void SubmitCaliberId()
        {
          
            string input = CaliberIdTextBox.Text;

            // Remove placeholder text
            if (input == "Indtast Kaliber-ID..." || string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Indtast venligst et gyldigt Kaliber-ID.", "Ugyldig Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Store the caliber ID and close the window
            CaliberId = input.Trim();
            DialogResult = true;
            this.Close();
        }
    }
}
