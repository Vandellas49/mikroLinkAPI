using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Teams : Entity
    {
        public Teams()
        {
            AccountAuthority = new HashSet<AccountAuthority>();
        }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int NumberOfCar { get; set; }
        public Company Company { get; set; }
        public ICollection<AccountAuthority> AccountAuthority { get; set; }

    }
}
