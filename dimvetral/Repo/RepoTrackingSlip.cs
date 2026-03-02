using dimvetral.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Repo
{
    internal class RepoTrackingSlip
    {
        public List<string> RepoHistoryList;
        public List<Models.CaliberTrackingSlip> trackingSlips;
        public void add(Models.CaliberTrackingSlip trackingSlip)
        {
            trackingSlips.Add(trackingSlip);
        }
        public List<Models.CaliberTrackingSlip> getAll()
        {
            return trackingSlips;
        }
        public Models.CaliberTrackingSlip getById(string id)
        {
            return trackingSlips.Find(t => t.CaliberTrackingSlipID == id);
        }
        public List<Models.CaliberTrackingSlip> getByLocation(string location)
        {
            return trackingSlips.FindAll(t => t.Location == location);
        }
        public List<string> GetHistory(string id)
        {
            var trackingSlip = getById(id);
            if (trackingSlip != null)
            {
                return trackingSlip.HistoryList;
            }

            string historyEntry = $"Tracking slip with ID {id} not found.";
            return new List<string> { historyEntry };
        }
    }
}
