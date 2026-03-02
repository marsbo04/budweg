using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    internal class CaliberTrackingSlip
    {
        private string CaliberTrackingSlipID;
        private string CaliberTrackingSlipName;
        private string History;
        private string Location;
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

        }
        public CaliberTrackingSlip(string CaliberTrackingSlipID, string Location)
        {
            this.CaliberTrackingSlipID = CaliberTrackingSlipID;
            this.Location = Location;

        }
    }
}
