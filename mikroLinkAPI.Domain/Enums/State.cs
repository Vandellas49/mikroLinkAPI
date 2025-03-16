using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace mikroLinkAPI.Domain.Enums
{
    public enum State
    {
        [Display(Name = "Manual Ekleme")]
        Manual = 0,
        [Display(Name = "Excel Ekleme")]
        Excel = 6,
        [Display(Name = "Talep")]
        Request = 1,
        [Display(Name = "Güncelleme")]
        Update = 2,
        [Display(Name = "Talep Üstüne Ekleme")]
        AddingOn = 3,
        [Display(Name = "Talep Malzeme Eksilmesi")]
        MalzemeVerilmesi = 4,
        [Display(Name = "Doğrulama")]
        Verification = 5,
        [Display(Name = "Randevu")]
        Randevu = 7
    }
}
