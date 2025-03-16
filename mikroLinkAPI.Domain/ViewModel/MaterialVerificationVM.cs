namespace mikroLinkAPI.Domain.ViewModel
{
    public class MaterialVerificationVM: ViewModelBase
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public string ComponentId { get; set; }
        public string SeriNo { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public int? MaterialType { get; set; }
        public string Shelf { get; set; }
    }
}
