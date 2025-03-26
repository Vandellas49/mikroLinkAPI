using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Rules
{
    public class MalzemeTuruKontrolRule(int malzemeTuru) : IRule
    {
        public Task<bool> IsValidAsync()
        {
            return Task.FromResult(malzemeTuru ==0||malzemeTuru==1);
        }

        public string ErrorMessage => "Malzeme Türü geçerli değil.";
    }
}
