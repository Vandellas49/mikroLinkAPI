using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.Rules.Interface;
namespace mikroLinkAPI.Domain.Rules
{
    public class SeriMalzemeKontrolRule(string componentId, int saglam, int arzali, int hurda, Component component) : IRule
    {
        public async Task<bool> IsValidAsync()
        {
            if (component == null || component.MalzemeTuru != (int)MalzemeTuru.Seri)
                return true;
            return await Task.FromResult((saglam + arzali + hurda) == 1);
        }

        public string ErrorMessage => $"Parça kodu ({componentId}) Seri ise Sağlam + Arızalı + Hurda toplamı 1 olmalıdır.";
    }
}
