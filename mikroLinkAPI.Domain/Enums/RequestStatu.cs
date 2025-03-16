using System.ComponentModel.DataAnnotations;
namespace mikroLinkAPI.Domain.Enums
{
    public enum RequestStatu
    {
        [Display(Name = "Superisor Talep")]
        StockMan = 0,
        [Display(Name = "Malzeme Takım Liderinde")]
        TeamLeader = 1,
        [Display(Name = "Tamamlandı")]
        Complate = 2,
        [Display(Name = "Depocu İptali")]
        StockManReject = 3,
        [Display(Name = "Takım Lideri İptali")]
        TeamLeaderReject = 4,
        [Display(Name = "Karşı Depocunun İptali")]
        OtherStockManReject = 5,
        [Display(Name = "Admin İptali")]
        AdminReject = 6,
        [Display(Name = "Manager İptali")]
        ManagerReject = 7,
    }
}
