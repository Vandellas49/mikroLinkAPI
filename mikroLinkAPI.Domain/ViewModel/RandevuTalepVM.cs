
namespace mikroLinkAPI.Domain.ViewModel
{
    public class RandevuTalepVM : ViewModelBase
    {
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string End { get; set; }
        public int TeamLeaderId { get; set; }
        public string TeamLeaderName { get; set; }
    }
    public class RandevuTalepByDateVM : ViewModelBase
    {
        public string Time { get; set; }
        public bool IsTaken { get; set; }
    }
}
