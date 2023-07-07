using FluentAssertions;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Business.Rules
{
    public class AustralianPhoneRuleTest
    {
        [Theory]
        [InlineData("1111", false)]
        [InlineData("1234567890", false)]
        [InlineData("045122381a", false)]
        [InlineData("0451223810", true)]
        [InlineData("+61451223810", true)]
        [InlineData("0196173903", false)]
        [InlineData("0296173903", true)]
        [InlineData("0396173903", true)]
        [InlineData("0596173903", false)]
        [InlineData("0796173903", true)]
        [InlineData("0896173903", true)]
        public void IsQualifiedFor_ShouldReturnCorrectly (string inputPhone, bool expected)
        {
            var rule = new AustralianPhoneRule();

            var result = rule.IsQualifiedFor (inputPhone);

            result.Should().Be (expected);
        }
    }
}
