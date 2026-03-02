using dimvetral.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Repo
{
    internal class RepoTrackingSlip
    {
        public List<string> RepoHistoryList { get; set; }
        public List<CaliberTrackingSlip> trackingSlips { get; set; }

        public RepoTrackingSlip()
        {
            RepoHistoryList = new List<string>();
            trackingSlips = new List<CaliberTrackingSlip>();
        }

        public void add(CaliberTrackingSlip trackingSlip)
        {
            trackingSlips.Add(trackingSlip);
        }
        
        public List<CaliberTrackingSlip> getAll()
        {
            return trackingSlips;
        }
        
        public CaliberTrackingSlip? getById(string id)
        {
            return trackingSlips.Find(t => t.CaliberTrackingSlipID == id);
        }
        
        public List<CaliberTrackingSlip> getByLocation(string location)
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
