namespace mikroLinkAPI.Domain.ViewModel
{
    public class RequestsModelVM: ViewModelBase
    {
        public int RequestStatu { get; set; }
        public int RequestType { get; set; }
        public TeamLeaderVM TeamLeader { get; set; }
        public int MaterialCount { get; set; }
        public TeamLeaderVM CreatedBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestDestination { get; set; }
        public string Sender { get; set; }
        public string WorkOrderNo { get; set; }
        public string Aciklama { get; set; }
    }
}
