using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.Rules.Interface;


namespace mikroLinkAPI.Domain.Rules
{
    public class ComponentSerialExistsRule : IRule
    {
        private readonly string _serialNumber;
        private readonly IMetarialRepository _metarialRepository;

        public ComponentSerialExistsRule(string serialNumber, IMetarialRepository metarialRepository)
        {
            _serialNumber = serialNumber;
            _metarialRepository = metarialRepository;
        }

        public async Task<bool> IsValidAsync()
        {
            return !await _metarialRepository.AnyAsync(p=>p.SeriNo==_serialNumber);
        }

        public string ErrorMessage => $"Seri numarası ({_serialNumber}) ComponentSerial tablosunda zaten mevcut.";
    }
}
