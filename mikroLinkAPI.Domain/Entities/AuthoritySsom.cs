using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class AuthoritySsom:Entity
    {
        public AuthoritySsom()
        {
            AccountAuthority = new HashSet<AccountAuthority>();
        }
        public  string UygulamaKodu { get; set; }
        public  string YetkiKodu { get; set; }

        public  ICollection<AccountAuthority> AccountAuthority { get; set; }
    }
}
