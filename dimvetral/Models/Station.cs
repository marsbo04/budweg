using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models
{
    public class Station
    {
        internal int StationID;

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string BuildingName { get; set; }

        public Station(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public bool isActive(DateTime at)
        {
            return at >= StartDate && at <= EndDate;
        }

        public string fullDisplayName()
        {
            return string.IsNullOrEmpty(BuildingName) 
                ? Name 
                : $"{Name} - {BuildingName}";
        }

        public bool belongsTo(Building building)
        {
            return building != null && BuildingName == building.Name;
        }
    }
}
