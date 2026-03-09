using dimvetral.Models;
using dimvetral.Models.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace dimvetral.ViewModels
{
    public class CreateTrackingSlipViewModel : INotifyPropertyChanged
    {
        private readonly IStationRepository _stationRepository;
        private readonly ITrackingSlipRepository _trackingSlipRepository;
        private int _selectedStationID;
        private DateTime _startDate;
        private bool _status;
        private string _errorMessage;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CreateTrackingSlipViewModel(IStationRepository stationRepository, ITrackingSlipRepository trackingSlipRepository)
        {
            _stationRepository = stationRepository;
            _trackingSlipRepository = trackingSlipRepository;
            _startDate = DateTime.Now;
        }

        public int selectedStationID
        {
            get => _selectedStationID;
            set
            {
                _selectedStationID = value;
                OnPropertyChanged();
            }
        }

        public DateTime startDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public bool status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public void createTrackingSlip()
        {
            if (!validateInput())
            {
                return;
            }

            clearError();
        }



        public List<Station> getStations()
        {
            return _stationRepository.GetAll();
        }

        public void setError(string message)
        {
            _errorMessage = message;
            OnPropertyChanged(nameof(_errorMessage));
        }

        public void clearError()
        {
            _errorMessage = string.Empty;
            OnPropertyChanged(nameof(_errorMessage));
        }

        private bool validateInput()
        {
            if (selectedStationID <= 0)
            {
                setError("Please select a valid station.");
                return false;
            }
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}