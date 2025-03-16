namespace mikroLinkAPI.Domain.Rules.Interface
{
    public interface IRule
    {
       Task<bool> IsValidAsync();
        string ErrorMessage { get; }
    }
}
