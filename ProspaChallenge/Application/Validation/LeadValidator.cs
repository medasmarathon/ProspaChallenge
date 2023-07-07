using FluentValidation;
using FluentValidation.Validators;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Business.Rules;

namespace ProspaChallenge.Application.Validation
{
    public class LeadValidator : AbstractValidator<Lead>
    {
        public LeadValidator(IPhoneRule phoneRule, ITimeTradingRule timeTradingRule, IBusinessNumberRule businessRule, IIndustryRule industryRule, ILoanAmountRule loanAmountRule)
        {
            RuleFor(lead => new { lead.FirstName, lead.LastName })
                .Must(x => !string.IsNullOrEmpty(x.FirstName + x.LastName))
                .WithMessage("Payload must have FirstName or LastName")
                .OverridePropertyName("FirstName, LastName");

            RuleFor(lead => new { lead.EmailAddress, lead.PhoneNumber })
                .Must(x => !string.IsNullOrEmpty(x.EmailAddress + x.PhoneNumber))
                .WithMessage("Payload must have Email or PhoneNumber")
                .OverridePropertyName("EmailAddress, PhoneNumber");

            RuleFor(lead => lead.EmailAddress).EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                .When(lead => !string.IsNullOrEmpty(lead.EmailAddress))
                .WithMessage("Not a valid EmailAddress")
                .WithState(lead => "Unknown");

            RuleFor(lead => lead.PhoneNumber).Must(phone => phoneRule.IsQualifiedFor(phone))
                .WithMessage("Not a valid Phone format")
                .WithState(lead => "Unknown");

            RuleFor(lead => lead.CitizenshipStatus).NotNull()
                .WithState(lead => "Unknown")
                .WithMessage("Not a valid CitizenshipStatus");

            RuleFor(lead => lead.CountryCode)
                .Cascade(CascadeMode.Stop)
                .Length(2)
                    .WithState(lead => "Unknown")
                    .WithMessage("Not a valid CountryCode")
                .Must(code => code == "AU")
                    .WithState(lead => "Unqualified")
                    .WithMessage("Not a recognized CountryCode");

            RuleFor(lead => lead.TimeTrading).Must(timeTrading => 
                timeTradingRule.IsQualifiedFor(timeTrading)
            )
                .WithMessage("Out of range TimeTrading value")
                .WithState(lead => "Unqualified");


            RuleFor(lead => lead.LoanAmount).Must(loanAmount =>
                loanAmountRule.IsQualifiedFor(loanAmount)
            )
                .WithMessage("Out of range LoanAmount value")
                .WithState(lead => "Unqualified");

            RuleFor(lead => lead.BusinessNumber).MustAsync(
                async (num, token) =>
                    await businessRule.IsQualifiedForAsync(num))
                .WithMessage("BusinessNumber is incorrect")
                .WithState(lead => "Unqualified");

            WhenAsync(
                // IF: Is in Qualified list
                async (lead, token) => await industryRule.IsQualifiedForAsync(lead.Industry),
                () => { }
            )
            .Otherwise(() =>
            {
                // IF: Not in Qualified list
                RuleFor(lead => lead.Industry)
                    .Cascade(CascadeMode.Stop)
                    // THEN: Should not be in Unqualified list
                    .MustAsync(
                        async (ind, token) =>
                            !(await industryRule.IsUnqualifiedForAsync(ind))
                    )
                        // Error message if be in Unqualified list
                        .WithState(lead => "Unqualified")
                        .WithMessage("Industry name is not allowed")
                    .Must(ind => false)
                        // Error message if not in either Qualified list or Unqualified list
                        .WithState(lead => "Unknown")
                        .WithMessage("Industry name is invalid");
            });
        }

    }
}
