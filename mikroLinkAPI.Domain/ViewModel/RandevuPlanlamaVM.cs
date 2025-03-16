
namespace mikroLinkAPI.Domain.ViewModel
{
    public class RandevuPlanlamaVM : ViewModelBase
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        private DateTime randevuTarihi;
        public DateTime RandevuTarihi
        {
            get
            {
                if (!string.IsNullOrEmpty(Date))
                    return DateTime.Parse(Date);
                return randevuTarihi;
            }
            set
            {
                Date = value.ToString();
                randevuTarihi = value;
            }
        }
    }
}
