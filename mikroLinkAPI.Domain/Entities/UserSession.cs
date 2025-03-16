using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public class UserSession:Entity
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public double TotalDuration { get; set; }
    }
}
