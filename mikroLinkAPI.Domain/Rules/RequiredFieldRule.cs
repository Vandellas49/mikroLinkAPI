using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Rules
{
    public class RequiredFieldRule : IRule
    {
        private readonly string _fieldValue;
        private readonly string _fieldName;

        public RequiredFieldRule(string fieldValue, string fieldName)
        {
            _fieldValue = fieldValue;
            _fieldName = fieldName;
        }

        public Task<bool> IsValidAsync()
        {
            return Task.FromResult(!string.IsNullOrEmpty(_fieldValue));
        }

        public string ErrorMessage => $"{_fieldName} alanı boş bırakılamaz.";
    }
}
