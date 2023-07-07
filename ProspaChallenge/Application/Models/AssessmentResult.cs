using ProspaChallenge.Business.Interfaces;

namespace ProspaChallenge.Application.Models
{
    public class AssessmentResult
    {
        public string Decision { get; set; } = "Unknown";
        public List<FieldValidationResult>? ValidationResult { get; set; }
    }

    public class FieldValidationResult
    {
        public string Decision { get; set; } = "Unknown";
        public string Rule { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}