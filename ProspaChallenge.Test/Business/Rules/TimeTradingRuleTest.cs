using FluentAssertions;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Business.Rules
{
    public class TimeTradingRuleTest
    {
        [Theory]
        [InlineData(-1, false)]
        [InlineData(1, true)]
        [InlineData(21, false)]
        public void IsQualifiedFor_ShouldReturnCorrectly (float timetrading, bool expected)
        {
            var rule = new TimeTradingRule();

            var result = rule.IsQualifiedFor (timetrading);

            result.Should().Be (expected);
        }
    }
}
