using dimvetral.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models.Repo
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
        

    }
}
