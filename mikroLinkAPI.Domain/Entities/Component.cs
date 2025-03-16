using mikroLinkAPI.Domain.Abstractions;


namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Component
    {
        public Component()
        {
            ComponentSerial = new HashSet<ComponentSerial>();
            RequestedMaterial = new HashSet<RequestedMaterial>();
        }
        public  string EquipmentDescription { get; set; }
        public int MalzemeTuru { get; set; }
        public string Id { get; set; }
        public  ICollection<ComponentSerial> ComponentSerial { get; set; }
        public  ICollection<RequestedMaterial> RequestedMaterial { get; set; }
    }
}
