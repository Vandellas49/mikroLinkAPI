namespace mikroLinkAPI.Domain.ViewModel
{
    public class MaterialsRequestVM
    {
        public int MaterialType { get; set; }
        public string EquipmentDescription { get; set; }
        public string ComponentId { get; set; }
        public int Scrap { get; set; }
        public int MaxScrap { get; set; }
        public int Sturdy { get; set; }
        public int MaxSturdy { get; set; }
        public int Defective { get; set; }
        public int MaxDefective { get; set; }
        public int Eklenen { get; set; }
    }
}
