using System.ComponentModel.DataAnnotations;

namespace mikroLinkAPI.Domain.Enums
{
    public enum MaterialTypeList
    {
        [Display(Name = "ROLLOUT")]
        ROL = 1,
        [Display(Name = "REV-TRANSPORT")]
        REV_TRANSPORT = 2,
        [Display(Name = "MOBİL")]
        MOB = 3,
        [Display(Name = "FIBER")]
        FIB = 4,
        [Display(Name = "REV-FİELD")]
        REV_FIELD = 5,
        [Display(Name = " BAKIM/ARIZA")]
        BAKIM_ARIZA = 6,
        [Display(Name = "REV-VFNET")]
        REV_VFNET = 7,
        [Display(Name = "HURDA")]
        HURDA = 8,
        [Display(Name = "ALLLOCATION")]
        ALLLOCATION = 9,
        [Display(Name = "HUAWEİ")]
        HUAWEI = 10,
        [Display(Name = "OLDER")]
        OLDER = 11,
        [Display(Name = "SAYIM")]
        SAYIM = 12,
        [Display(Name = "TANIMSIZ")]
        TANIMSIZ = 13,
        [Display(Name = "MRN TAKIP")]
        MRN_TAKIP = 14,
        [Display(Name = "EBAND")]
        EBAND = 15,
        [Display(Name = "IADE")]
        IADE = 16,
        [Display(Name = "ÖZEL PROJE")]
        OzelProje = 17

    }
}
