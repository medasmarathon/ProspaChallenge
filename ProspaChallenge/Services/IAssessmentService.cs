using ProspaChallenge.Application.Models;
using ProspaChallenge.Business.Models;

namespace ProspaChallenge.Services
{
    public interface IAssessmentService
    {
        Task<AssessmentResult> Assess(Lead lead);
    }
}
