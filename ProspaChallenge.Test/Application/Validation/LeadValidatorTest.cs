using FluentValidation.TestHelper;
using Moq;
using ProspaChallenge.Application.Validation;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Business.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Application.Validation
{
    public class LeadValidatorTest
    {
        [Fact]
        public async Task ShouldValidate_FirstNameLastName_Correctly()
        {
            var leadWithNoFirstNameLastName = new Lead();
            var leadWithFirstNameOnly = new Lead() { FirstName = "aa" };
            var leadWithLastNameOnly = new Lead() { LastName = "aa" };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithNoFirstNameLastName);
            result.ShouldHaveValidationErrorFor("FirstName, LastName");
            result = await validator.TestValidateAsync(leadWithLastNameOnly);
            result.ShouldNotHaveValidationErrorFor("FirstName, LastName");
            result = await validator.TestValidateAsync(leadWithFirstNameOnly);
            result.ShouldNotHaveValidationErrorFor("FirstName, LastName");
        }

        [Fact]
        public async Task ShouldValidate_EmailAndPhone_Correctly()
        {
            var leadWithNoEmailOrPhone = new Lead();
            var leadWithEmail = new Lead() { EmailAddress = "aa@dd.com" };
            var leadWithPhone = new Lead() { PhoneNumber = "0396173903" };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithNoEmailOrPhone);
            result.ShouldHaveValidationErrorFor("EmailAddress, PhoneNumber");
            result = await validator.TestValidateAsync(leadWithPhone);
            result.ShouldNotHaveValidationErrorFor("EmailAddress, PhoneNumber");
            result = await validator.TestValidateAsync(leadWithEmail);
            result.ShouldNotHaveValidationErrorFor("EmailAddress, PhoneNumber");
        }

        [Theory]
        [InlineData("aaa", false)]
        [InlineData("aaa@bb.com", true)]
        [InlineData("aaa@bb", true)]
        public async Task ShouldValidate_Email_Correctly(string email, bool expected)
        {
            var leadWithEmail = new Lead() { EmailAddress = email };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithEmail);
            if (expected)
                result.ShouldNotHaveValidationErrorFor(l => l.EmailAddress);
            else
                result.ShouldHaveValidationErrorFor(l => l.EmailAddress);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(CitizenshipStatus.PermanentResident, true)]
        [InlineData(CitizenshipStatus.Citizen, true)]
        public async Task ShouldValidate_CitizenshipStatus_Correctly(CitizenshipStatus? status, bool expected)
        {
            var leadWithCitizenship = new Lead() { CitizenshipStatus = status };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithCitizenship);
            if (expected)
                result.ShouldNotHaveValidationErrorFor(l => l.CitizenshipStatus);
            else
                result.ShouldHaveValidationErrorFor(l => l.CitizenshipStatus);
        }

        [Theory]
        [InlineData("aaa", false)]
        [InlineData("AU", true)]
        public async Task ShouldValidate_CountryCode_Correctly(string code, bool expected)
        {
            var leadWithCountry = new Lead() { CountryCode = code };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithCountry);
            if (expected)
                result.ShouldNotHaveValidationErrorFor(l => l.CountryCode);
            else
                result.ShouldHaveValidationErrorFor(l => l.CountryCode);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ShouldValidate_TimeTrading_Correctly(bool ruleCheckPassed)
        {
            var timeTradingRule = new Mock<ITimeTradingRule>();
            timeTradingRule.Setup(r => r.IsQualifiedFor(It.IsAny<float>())).Returns(ruleCheckPassed);
            var leadWithTimeTrading = new Lead() { TimeTrading = 10000 };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                timeTradingRule.Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithTimeTrading);
            if (ruleCheckPassed)
                result.ShouldNotHaveValidationErrorFor(l => l.TimeTrading);
            else
                result.ShouldHaveValidationErrorFor(l => l.TimeTrading);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ShouldValidate_LoanAmount_Correctly(bool ruleCheckPassed)
        {
            var loanAmountRule = new Mock<ILoanAmountRule>();
            loanAmountRule.Setup(r => r.IsQualifiedFor(It.IsAny<decimal>())).Returns(ruleCheckPassed);
            var leadWithLoanAmount = new Lead() { LoanAmount = 10000 };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                new Mock<IIndustryRule>().Object,
                loanAmountRule.Object
                );

            var result = await validator.TestValidateAsync(leadWithLoanAmount);
            if (ruleCheckPassed)
                result.ShouldNotHaveValidationErrorFor(l => l.LoanAmount);
            else
                result.ShouldHaveValidationErrorFor(l => l.LoanAmount);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task ShouldValidate_BusinessNumber_Correctly(bool ruleCheckPassed)
        {
            var businessNumRule = new Mock<IBusinessNumberRule>();
            businessNumRule.Setup(r => r.IsQualifiedForAsync(It.IsAny<string>())).ReturnsAsync(ruleCheckPassed);
            var leadWithBusinessNumber = new Lead() { BusinessNumber = "12314312" };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                businessNumRule.Object,
                new Mock<IIndustryRule>().Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithBusinessNumber);
            if (ruleCheckPassed)
                result.ShouldNotHaveValidationErrorFor(l => l.BusinessNumber);
            else
                result.ShouldHaveValidationErrorFor(l => l.BusinessNumber);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task ShouldValidate_Industry_Correctly(bool ruleQualifiedPassed, bool ruleUnqualifiedPassed)
        {
            var industryRule = new Mock<IIndustryRule>();
            industryRule.Setup(r => r.IsQualifiedForAsync(It.IsAny<string>())).ReturnsAsync(ruleQualifiedPassed);
            industryRule.Setup(r => r.IsUnqualifiedForAsync(It.IsAny<string>())).ReturnsAsync(ruleUnqualifiedPassed);
            var leadWithIndustry = new Lead() { Industry = "12314312" };

            var validator = new LeadValidator(
                new Mock<IPhoneRule>().Object,
                new Mock<ITimeTradingRule>().Object,
                new Mock<IBusinessNumberRule>().Object,
                industryRule.Object,
                new Mock<ILoanAmountRule>().Object
                );

            var result = await validator.TestValidateAsync(leadWithIndustry);
            if (ruleQualifiedPassed)
                result.ShouldNotHaveValidationErrorFor(l => l.Industry);
            else
            {
                if (ruleUnqualifiedPassed)
                    result.ShouldHaveValidationErrorFor(l => l.Industry).WithCustomState("Unqualified");
                else
                    result.ShouldHaveValidationErrorFor(l => l.Industry).WithCustomState("Unknown");
            }
        }
    }
}
