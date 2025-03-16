namespace mikroLinkAPI.Domain.ViewModel
{
    public class TeamLeaderVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
    public class CompanyTeamLeader
    {
        public string Company { get; set; }
        public List<TeamLeaderVM> TeamLeaders { get; set; }
    }
}
