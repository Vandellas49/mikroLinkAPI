namespace mikroLinkAPI.Domain.ViewModel
{
    public class SiteVM: ViewModelBase
    {
        public string PlanId { get; set; }
        public string SiteId { get; set; }
        public string Region { get; set; }
        public string SiteName { get; set; }
        public string SiteTip { get; set; }
        public int? IlId { get; set; }
        public string KordinatN { get; set; }
        public string KordinatE { get; set; }
        public IlVM Il { get; set; }
        public bool IsEditable { get; set; }

    }
}
