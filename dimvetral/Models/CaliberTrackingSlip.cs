using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    public class CaliberTrackingSlip
    {
        public string CaliberTrackingSlipID { get; private set; }
        private string CaliberTrackingSlipName;
        private string History;
        public List<string> HistoryList { get; private set; }
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
            this.HistoryList = new List<string>();
            UpdateHistory(History);
        }
        
        public CaliberTrackingSlip(string CaliberTrackingSlipID, string Location)
        {
            this.CaliberTrackingSlipID = CaliberTrackingSlipID;
            this.Location = Location;
            this.HistoryList = new List<string>(); 
        }
        
        public void UpdateHistory(string newEntry)
        {
            if (!string.IsNullOrEmpty(newEntry))
            {
                HistoryList.Add(newEntry);
                History += $"{newEntry}{Environment.NewLine}";
            }
        }
    }
}
