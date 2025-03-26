using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Rules
{
    public class UniqueComponentIdRule(string componentId, Dictionary<string, int> componentCounts) : IRule
    {

        public Task<bool> IsValidAsync()
        {
            return Task.FromResult(!(componentCounts.ContainsKey(componentId) && componentCounts[componentId] > 1));
        }

        public string ErrorMessage => $"Parça Kodu ({componentId}) zaten mevcut.";
    }
}
