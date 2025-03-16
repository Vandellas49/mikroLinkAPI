using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Rules.Interface;
namespace mikroLinkAPI.Domain.Rules
{
    public class SarfMalzemeKontrolRule(string componentId, string serialNumber, Component component) : IRule
    {
        public async Task<bool> IsValidAsync()
        {
            if (component == null || component.MalzemeTuru != (int)MalzemeTuru.Sarf)
                return true;
            return await Task.FromResult( serialNumber == "SarfMalzeme");
        }

        public string ErrorMessage => $"Parça kodu ({componentId}) Sarf olarak tanımlanmış ama Seri numarası ({serialNumber}) 'SarfMalzeme' değil.";
    }
}
