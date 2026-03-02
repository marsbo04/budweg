using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    internal class TrackingSlipService
    {
        public CaliberTrackingSlip createTrackingSlip(int stationID, string location)
        {
            string trackingSlipID = Guid.NewGuid().ToString();
            string SationID  = $"Tracking Slip for Station {stationID}";
            CaliberTrackingSlip newCal = new CaliberTrackingSlip(trackingSlipID, SationID);
            return newCal;
        }
    }
}
