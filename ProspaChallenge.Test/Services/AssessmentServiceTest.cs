using FluentAssertions;
using FluentValidation;
using Moq;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Services
{
    public class AssessmentServiceTest
    {
        [Theory]
        [InlineData(false, "Unqualified")]
        [InlineData(false, "Unknown")]
        [InlineData(false, "")]
        [InlineData(true, "")]
        public async Task Assess_ShouldReturnCorrectly(bool validateResultPassed, string customState)
        {
            var validator = new InlineValidator<Lead>();
            validator.RuleFor(l => l)
                .MustAsync((l, token) => Task.FromResult(validateResultPassed))
                .WithState(l => customState);

            var service = new AssessmentService(validator);
            var result = await service.Assess(new Lead());

            if (validateResultPassed)
            {
                result.Should().NotBeNull();
                result.ValidationResult.Should().BeNull();
                result.Decision.Should().Be("Qualified");
            } else
            {
                var isUnknownState = string.IsNullOrEmpty(customState) || customState == "Unknown";
                result.ValidationResult.Should().NotBeNullOrEmpty();
                result.Decision.Should().Be(isUnknownState ? "Unknown" : "Unqualified");
            }
        }
    }
}
