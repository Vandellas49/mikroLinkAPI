using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Rules.Interface;
namespace mikroLinkAPI.Domain.Rules
{
    public class ComponentKontrolRule(string componentId, Component component) : IRule
    {
        public async Task<bool> IsValidAsync()
        {
            return await Task.FromResult(component != null);
        }

        public string ErrorMessage => $"Parça kodu ({componentId}) bulunamadı.";
    }
}
