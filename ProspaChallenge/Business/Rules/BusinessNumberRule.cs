using ProspaChallenge.Business.Interfaces;
using System.Text.RegularExpressions;

namespace ProspaChallenge.Business.Rules
{
    public interface IBusinessNumberRule : IAsyncRule<string> { }
    public class BusinessNumberRule : IBusinessNumberRule
    {
        public async Task<bool> IsQualifiedForAsync(string businessNumber)
        {
            await Task.Delay(2000);
            return businessNumber.Length == 11 && Regex.IsMatch(businessNumber, @"^\d+$");
        }

        public Task<bool> IsUnqualifiedForAsync(string validationTarget)
        {
            throw new NotImplementedException();
        }
    }
}
