using mikroLinkAPI.Domain.Abstractions;


namespace mikroLinkAPI.Domain.Entities
{
    public class Smsverification : Entity
    {
        public string UserName { get; set; }
        public DateTime CurrentDate { get; set; }
        public int VerificationCode { get; set; }
        public string ApplicationCode { get; set; }
    }
}
