using dimvetral.Models;
using System.Collections.Generic;

namespace dimvetral.Models.Repo
{
    public interface ITrackingSlipRepository
    {
        CaliberTrackingSlip GetById(string id);
        List<CaliberTrackingSlip> GetAll();
        List<CaliberTrackingSlip> GetByLocation(string location);
        List<Station> GetHistory(string trackingSlipId);
        void Add(CaliberTrackingSlip slip);
        bool Exists(string id);
    }
}