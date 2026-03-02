using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    internal class CaliberTrackingSlip
    {
        public string CaliberTrackingSlipID { get; private set; }
        private string CaliberTrackingSlipName;
        private string History;
        public List<string> HistoryList; 
        public string Location { get; private set; }
        private string Status;
        private string Warehouse;
        private DateTime StartDate;

        public CaliberTrackingSlip(string CaliberTrackingSlipID, string CaliberTrackingSlipName, string History, string Location, string Status, string Warehouse, DateTime StartDate)
        {
            this.CaliberTrackingSlipID = CaliberTrackingSlipID;
            this.CaliberTrackingSlipName = CaliberTrackingSlipName;
            this.History = History;
            this.Location = Location;
            this.Status = Status;
            this.Warehouse = Warehouse;
            this.StartDate = StartDate;
            UpdateHistory(History);

        }
        public CaliberTrackingSlip(string CaliberTrackingSlipID, string Location)
        {
            this.CaliberTrackingSlipID = CaliberTrackingSlipID;
            this.Location = Location;

        }
        public void UpdateHistory(string newEntry)
        {
            HistoryList.Add(newEntry);
            History += $"{newEntry}{Environment.NewLine}";
        }

    }
}
