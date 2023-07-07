using FluentValidation;
using ProspaChallenge.Application.Models;
using ProspaChallenge.Business.Models;

namespace ProspaChallenge.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IValidator<Lead> _validator;
        public AssessmentService(IValidator<Lead> validator)
        {
            _validator = validator;
        }
        public async Task<AssessmentResult> Assess(Lead lead)
        {
            var validationResult = await _validator.ValidateAsync(lead);
            if (validationResult == null)
                return new AssessmentResult();

            if (validationResult.IsValid)
                return new AssessmentResult()
                {
                    Decision = "Qualified"
                };

            var errorStates = validationResult.Errors
                .Select(x => x.CustomState.ToString()).ToList();

            var decision = string.Empty;
            if (errorStates.All(state => state == "Qualified"))
            {
                decision = "Qualified";
            }
            else
            {
                if (errorStates.Any(s => s == "Unknown" || string.IsNullOrEmpty(s)))
                {
                    decision = "Unknown";
                }
                else
                    decision = "Unqualified";
            }
            return new AssessmentResult()
            {
                Decision = decision,
                ValidationResult = validationResult.Errors.Select(err => new FieldValidationResult
                {
                    Message = err.ErrorMessage,
                    Rule = err.PropertyName + " Rule",
                    Decision = err.CustomState == null ? "Qualified" : err.CustomState.ToString()
                }).ToList()
            };
        }
    }
}
