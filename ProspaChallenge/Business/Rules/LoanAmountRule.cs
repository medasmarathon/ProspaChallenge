using Microsoft.AspNetCore.Rewrite;
using ProspaChallenge.Business.Interfaces;

namespace ProspaChallenge.Business.Rules
{
    public interface ILoanAmountRule : IRule<decimal> { }
    public class LoanAmountRule : ILoanAmountRule
    {
        public bool IsQualifiedFor(decimal loanAmount)
        {
            return loanAmount > 10_000 && loanAmount < 100_000;
        }

        public bool IsUnqualifiedFor(decimal validationTarget)
        {
            throw new NotImplementedException();
        }
    }
}
