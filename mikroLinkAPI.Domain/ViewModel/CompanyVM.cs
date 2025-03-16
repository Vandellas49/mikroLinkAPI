

namespace mikroLinkAPI.Domain.ViewModel
{
    public class CompanyVM: ViewModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int IlId { get; set; }
        public IlVM Il { get; set; }
        public bool IsEditable { get; set; }
    }
}
