using System;
using System.Collections.Generic;
using System.Text;

namespace dimvetral.Models.Repo
{
    internal class RepoStation : IStationRepository
    {
        private List<Station> stations;
        public RepoStation()
        {
            stations = new List<Station>();
        }
        public void add(Station station)
        {
            stations.Add(station);
        }
        public List<Station> GetAll()
        {
            return stations;
        }
        public Station GetById(int id)
        {
            return stations.Find(s => s.StationID == id);
        }
        public List<Station> GetByName(string name)
        {
            return stations.FindAll(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
