using System.Windows;
using System.Windows.Controls;
using wpfnavigation.viewmodels;

namespace wpfnavigation.views
{
    public partial class CreateDeliveryNoteView : UserControl
    {
        public CreateDeliveryNoteView()
        {
            InitializeComponent();
        }

        private void KaliberDel1_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateDeliveryNoteViewModel viewModel)
            {
                viewModel.Name = "KaliberDel1";
            }
        }

        private void KaliberDel2_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateDeliveryNoteViewModel viewModel)
            {
                viewModel.Name = "KaliberDel2";
            }
        }

        private void KaliberDel3_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CreateDeliveryNoteViewModel viewModel)
            {
                viewModel.Name = "KaliberDel3";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your logic for Cancel button click here
        }
    }
}