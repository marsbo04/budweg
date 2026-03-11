using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dimvetral.ViewModels
{
    public class RemarkInputViewModel : BaseViewModel
    {
        private string _enteredRemark;

        

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
    }
}