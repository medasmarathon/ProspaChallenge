using FluentAssertions;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Business.Rules
{
    public class LoanAmountRuleTest
    {
        [Theory]
        [InlineData(1111, false)]
        [InlineData(10_001, true)]
        [InlineData(99_001, true)]
        [InlineData(100_001, false)]
        public void IsQualifiedFor_ShouldReturnCorrectly (decimal loanAmount, bool expected)
        {
            var rule = new LoanAmountRule();

            var result = rule.IsQualifiedFor (loanAmount);

            result.Should().Be (expected);
        }
    }
}
