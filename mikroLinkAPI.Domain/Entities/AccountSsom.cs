using Microsoft.AspNetCore.Identity;
using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class AccountSsom: Entity
    {
        public AccountSsom()
        {
            AccountAuthority = [];
            ComponentSerialTeamLeader = [];
            ComponentSerialWhoDone = [];
            MaterialTracking = [];
            Randevu = [];
            RequestTeamLeader = [];
            RequestWhoCanceled = [];
            RequestWhoDone = [];
            StoreExitTeamLeader = [];
            StoreExitTeamLeaderIdExitNavigation = [];
            StoreInFromTeamLeader = [];
            StoreInTeamLeader = [];
            StoreInWhoDone = [];
        }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public byte[] Password { get; set; }
        public int CompanyId { get; set; }
        public string PhoneNumberTwo { get; set; }
        public Company Company { get; set; }
        public  ICollection<AccountAuthority> AccountAuthority { get; set; }
        public  ICollection<ComponentSerial> ComponentSerialTeamLeader { get; set; }
        public  ICollection<ComponentSerial> ComponentSerialWhoDone { get; set; }
        public  ICollection<MaterialTracking> MaterialTracking { get; set; }
        public  ICollection<Randevu> Randevu { get; set; }
        public  ICollection<Request> RequestTeamLeader { get; set; }
        public  ICollection<Request> RequestWhoCanceled { get; set; }
        public  ICollection<Request> RequestWhoDone { get; set; }
        public  ICollection<StoreExit> StoreExitTeamLeader { get; set; }
        public  ICollection<StoreExit> StoreExitTeamLeaderIdExitNavigation { get; set; }
        public  ICollection<StoreIn> StoreInFromTeamLeader { get; set; }
        public  ICollection<StoreIn> StoreInTeamLeader { get; set; }
        public  ICollection<StoreIn> StoreInWhoDone { get; set; }
        public  ICollection<StoreExit> StoreExitWhoDone { get; set; }
    }
}
