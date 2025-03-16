using Microsoft.AspNetCore.Identity;
using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class AccountAuthority:Entity
    {
        public int AuthorityId { get; set; }
        public int AccountId { get; set; }
        public int? TeamId { get; set; }
        public  AccountSsom Account { get; set; }
        public  AuthoritySsom Authority { get; set; }
        public  Teams Team { get; set; }
    }
}
