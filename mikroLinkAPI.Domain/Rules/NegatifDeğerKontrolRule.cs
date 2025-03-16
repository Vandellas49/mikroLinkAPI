using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Rules
{
    public class NegatifDeğerKontrolRule : IRule
    {
        private readonly int _saglam;
        private readonly int _arzali;
        private readonly int _hurda;

        public NegatifDeğerKontrolRule(int saglam, int arzali, int hurda)
        {
            _saglam = saglam;
            _arzali = arzali;
            _hurda = hurda;
        }

        public Task<bool> IsValidAsync()
        {
            return Task.FromResult(_saglam >= 0 && _arzali >= 0 && _hurda >= 0);
        }

        public string ErrorMessage => "Sağlam, Arızalı veya Hurda değerleri negatif olamaz.";
    }
}
