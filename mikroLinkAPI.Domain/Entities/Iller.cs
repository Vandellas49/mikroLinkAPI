
using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Iller: Entity
    {
        public Iller()
        {
            Company = new HashSet<Company>();
            Site = new HashSet<Site>();
        }
        public  string Sehir { get; set; }
        public  ICollection<Company> Company { get; set; }
        public  ICollection<Site> Site { get; set; }
    }
}
