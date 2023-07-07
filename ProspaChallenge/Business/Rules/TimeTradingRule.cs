using Microsoft.AspNetCore.Rewrite;
using ProspaChallenge.Business.Interfaces;

namespace ProspaChallenge.Business.Rules
{
    public interface ITimeTradingRule : IRule<float> { }
    public class TimeTradingRule : ITimeTradingRule
    {
        public bool IsQualifiedFor(float timeTrading)
        {
            return timeTrading > 0 && timeTrading < 20;
        }

        public bool IsUnqualifiedFor(float validationTarget)
        {
            throw new NotImplementedException();
        }
    }
}
