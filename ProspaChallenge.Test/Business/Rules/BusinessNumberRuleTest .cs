using FluentAssertions;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Business.Rules
{
    public class BusinessNumberRuleTest
    {
        [Theory]
        [InlineData("1111", false)]
        [InlineData("12345678901", true)]
        public async Task IsQualifiedFor_ShouldReturnCorrectly (string businessNumber, bool expected)
        {
            var rule = new BusinessNumberRule();

            var result = await rule.IsQualifiedForAsync (businessNumber);

            result.Should().Be (expected);
        }
    }
}
