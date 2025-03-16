using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Rules
{
    public class UniqueSerialNumberRule : IRule
    {
        private readonly string _serialNumber;
        private readonly Dictionary<string, int> _serialCounts;

        public UniqueSerialNumberRule(string serialNumber, Dictionary<string, int> serialCounts)
        {
            _serialNumber = serialNumber;
            _serialCounts = serialCounts;
        }

        public Task<bool> IsValidAsync()
        {
            return Task.FromResult(!(_serialCounts.ContainsKey(_serialNumber) && _serialCounts[_serialNumber] > 1));
        }

        public string ErrorMessage => $"Seri numarası ({_serialNumber}) zaten mevcut.";
    }
}
