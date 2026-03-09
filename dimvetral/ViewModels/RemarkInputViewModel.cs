using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dimvetral.ViewModels
{
    public class RemarkInputViewModel : INotifyPropertyChanged
    {
        private string _enteredRemark;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RemarkInputViewModel()
        {
        }

        public string EnteredRemark
        {
            get => _enteredRemark;
            set
            {
                _enteredRemark = value;
                OnPropertyChanged();
            }
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(EnteredRemark);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}