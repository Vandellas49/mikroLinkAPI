using mikroLinkAPI.Domain.Rules.Interface;

namespace mikroLinkAPI.Domain.Validators
{
    public class MaterialValidator
    {
        private readonly List<IRule> _rules = new List<IRule>();

        public MaterialValidator AddRule(IRule rule)
        {
            _rules.Add(rule);
            return this;
        }

        public async Task<(bool IsValid, List<string> Errors)> ValidateAsync()
        {
            var errors = new List<string>();

            foreach (var rule in _rules)
            {
                if (!(await rule.IsValidAsync()))
                {
                    errors.Add(rule.ErrorMessage);
                }
            }

            return (errors.Count == 0, errors);
        }
    }
}
