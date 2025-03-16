
using System.ComponentModel.DataAnnotations;

namespace mikroLinkAPI.Domain.ViewModel.ExcelModels
{
    public class VerificationCompanyEX
    {
        [Display(Name = "Parça Kodu")]
        public string ComponentId { get; set; }
        [Display(Name = "Ekipman Açıklaması")]
        public string EquipmentDescription { get; set; }
        [Display(Name = "Seri Numarası")]
        public string SeriNo { get; set; }
        [Display(Name = "İşlemi Gerçekleştiren")]
        public string CreatedBy { get; set; }
        [Display(Name = "Hurda")]
        public int Scrap { get; set; }
        [Display(Name = "Arzalı")]
        public int Defective { get; set; }
        [Display(Name = "Sağlam")]
        public int Sturdy { get; set; }
        [Display(Name = "Raf")]
        public string Shelf { get; set; }
        [Display(Name = "Tarih")]
        public DateTime Tarih { get; set; }
    }
}
