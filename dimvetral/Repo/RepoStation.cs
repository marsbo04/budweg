using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Repo
{
    internal class RepoStation
    {
        public List<Models.Station> stations;
        public void add(Models.Station station)
        {
            stations.Add(station);
        }
        public List<Models.Station> getAll()
        {
            return stations;
        }
        public Models.Station getById(string id)
        {
            return stations.Find(s => s.StationID == id);
        }
        public List<Models.Station> getByName(string name)
            {
                return stations.FindAll(s => s.StationName == name);
        }
    }
}
