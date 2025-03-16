
namespace mikroLinkAPI.Domain.ViewModel
{
    public class AccountSsomVM:ViewModelBase
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumberTwo { get; set; }
        public CompanyVM Company { get; set; }
        public int AuthorityId { get; set; }
    }
}
