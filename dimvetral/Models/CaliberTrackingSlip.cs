namespace dimvetral.Models
{
    public class CaliberTrackingSlip
    {
        public string CaliberTrackingSlipID { get; private set; }
        private string CaliberTrackingSlipName;
        private string History;
        public List<string> HistoryList { get; private set; }
   
        private bool Status;
        private string Warehouse;
        private DateTime StartDate;

        public CaliberTrackingSlip(string id, string name, string history, bool status, string warehouse, DateTime startDate)
        {
            CaliberTrackingSlipID = id;
            CaliberTrackingSlipName = name;
            History = history;
            Status = status;
            Warehouse = warehouse;
            StartDate = startDate;
            HistoryList = new List<string>(history.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        }


    }
}
