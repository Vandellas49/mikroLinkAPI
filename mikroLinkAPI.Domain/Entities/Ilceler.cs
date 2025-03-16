using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Ilceler:Entity
    {
        public  string Ilce { get; set; }
        public int Sehir { get; set; }
    }
}
