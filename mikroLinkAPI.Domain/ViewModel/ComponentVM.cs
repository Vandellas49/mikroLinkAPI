using mikroLinkAPI.Domain.Enums;
using System.ComponentModel;
namespace mikroLinkAPI.Domain.ViewModel
{
    public class ComponentVM
    {
        [DisplayName("Parça Kodu")]
        public string Id { get; set; }
        [DisplayName("Malzeme Türü")]
        public MalzemeTuru MalzemeTuru { get; set; }
        [DisplayName("Ekipman Açıklaması")]
        public string EquipmentDescription { get; set; }
        public bool IsEditable { get; set; }
    }
}
