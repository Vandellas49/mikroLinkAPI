
namespace mikroLinkAPI.Domain.ViewModel
{
    public class ComponentSerialVM: ViewModelBase
    {
        public string Location { get; set; }
        public string SeriNo { get; set; }
        public string ComponentId { get; set; }
        public string GIrsaliyeNo { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public string Shelf { get; set; }
        public int MaterialType { get; set; }
        public string EquipmentDescription { get; set; }
        public int? CompanyId { get; set; }
        public int? SiteId { get; set; }
        public int? TeamLeaderId { get; set; }
        public DateTime StoreDate { get; set; }
        public int? WhoDoneId { get; set; }
        public SiteVM Site { get; set; }
        public CompanyVM Company { get; set; } 
        public AccountSsomVM TeamLeader { get; set; } 
    }
}
