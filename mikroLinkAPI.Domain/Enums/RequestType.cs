
using System.ComponentModel.DataAnnotations;

namespace mikroLinkAPI.Domain.Enums
{
    public enum RequestType
    {
        [Display(Name = "Firmadan Firmaya")]
        CompanytoCompany = 0,
        [Display(Name = "Firmadan Sahaya")]
        CompanytoSite = 1,
        [Display(Name = "Sahadan Firmaya")]
        SitetoCompany = 2,
        [Display(Name = "Sahadan Sahaya")]
        SitetoSite = 3,
    }
}
