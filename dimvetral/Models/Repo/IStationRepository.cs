using dimvetral.Models;
using System.Collections.Generic;

namespace dimvetral.Models.Repo
{
    public interface IStationRepository
    {
        List<Station> GetAll();
        Station GetById(int id);
        List<Station> GetByName(string name);
    }
}