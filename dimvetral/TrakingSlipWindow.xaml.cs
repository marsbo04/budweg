using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dimvetral.ViewModels;

namespace dimvetral
{

    /// Interaction logic for MainWindow.xaml

    public partial class TrakningSlipWindow : Window
    {
        private readonly TrakringSlipViewModel _viewModel;

        public TrakningSlipWindow()
        {
            InitializeComponent();
            _viewModel = new TrakringSlipViewModel();
            DataContext = _viewModel;
        }

        public TrakningSlipWindow(string employeeId) : this()
        {
            _viewModel.EmployeeId = employeeId;
        }
    }
}