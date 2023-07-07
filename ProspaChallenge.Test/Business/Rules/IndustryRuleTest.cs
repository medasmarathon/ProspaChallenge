using FluentAssertions;
using Moq;
using ProspaChallenge.Business.Interfaces;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Business.Rules
{
    public class IndustryRuleTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public async Task IsQualifiedFor_ShouldReturnCorrectly (string industry, List<string> listAllowed, List<string> listBanned, bool expectedQualified, bool expectedUnqualified)
        {
            var industryRepoMock = new Mock<IIndustryRepository>();
            industryRepoMock.Setup(rp => rp.GetAllowedIndustries()).ReturnsAsync(listAllowed);
            industryRepoMock.Setup(rp => rp.GetBannedIndustries()).ReturnsAsync(listBanned);

            var rule = new IndustryRule(industryRepoMock.Object);

            var isQualified = await rule.IsQualifiedForAsync (industry);
            var isUnqualified = await rule.IsUnqualifiedForAsync (industry);

            isQualified.Should().Be (expectedQualified);
            isUnqualified.Should().Be (expectedUnqualified);
        }

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { "1111", (new List<string>() { "aaa", "bbb" }), new List<string>() { "ccc", "ddd" }, false, false };
            yield return new object[] { "aaa", (new List<string>() { "aaa", "bbb" }), new List<string>() { "ccc", "ddd" }, true, false };
            yield return new object[] { "ddd", (new List<string>() { "aaa", "bbb" }), new List<string>() { "ccc", "ddd" }, false, true };
        }
    }
}
