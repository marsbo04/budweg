using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    internal class Station
    {
        public string StationID { get; internal set; }
        public string StationName{ get; internal set; }
       public string Status;

        Station(string Id, string name, string status) {
            this.StationID = StationID;
            this.StationName = StationName;
            this.Status = Status;
        }


    }
}
