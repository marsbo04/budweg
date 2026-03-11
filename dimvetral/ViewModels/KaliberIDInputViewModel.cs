using dimvetral.Models.Repo;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dimvetral.ViewModels
{
    public class KaliberIDInputViewModel : BaseViewModel
    {
        private readonly ITrackingSlipRepository _repository;
        private string _enteredID;

        public KaliberIDInputViewModel(ITrackingSlipRepository repository)
        {
            _repository = repository;
        }

        public string EnteredID
        {
            get => _enteredID;
            set
            {
                _enteredID = value;
                OnPropertyChanged();
            }
        }

        public bool Validate()
        {
            return !string.IsNullOrWhiteSpace(EnteredID) && _repository.Exists(EnteredID);
        }
    }
}