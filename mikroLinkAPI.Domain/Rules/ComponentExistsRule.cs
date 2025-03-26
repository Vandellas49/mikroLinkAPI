using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.Rules.Interface;


namespace mikroLinkAPI.Domain.Rules
{
    public class ComponentExistsRule(string componentId, IComponentRepository componentRepository) : IRule
    {

        public async Task<bool> IsValidAsync()
        {
            return !await componentRepository.AnyAsync(p=>p.Id== componentId);
        }
        public string ErrorMessage => $"Parça Kodu ({componentId}) zaten mevcut.";
    }
}
