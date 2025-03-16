using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.Rules.Interface;
namespace mikroLinkAPI.Domain.Rules
{
    public class SiteIdKontrol : IRule
    {
        private readonly int _siteId;
        private readonly ISiteRepository _siteRepository;

        public SiteIdKontrol(int siteId, ISiteRepository siteRepository)
        {
            _siteId = siteId;
            _siteRepository = siteRepository;
        }

        public async Task<bool> IsValidAsync()
        {
            var hasSite = await _siteRepository.AnyAsync(p=>p.Id== _siteId);
            return hasSite;
        }

        public string ErrorMessage => $"{_siteId} nolu saha bulunamadı";
    }
}
