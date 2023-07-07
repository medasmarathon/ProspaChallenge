using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProspaChallenge.Application.Models;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Services;

namespace ProspaChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        public LeadController(IAssessmentService assessmentService) 
        {
            _assessmentService = assessmentService;
        }
        [HttpPost]
        public async Task<JsonResult> Assessment(LeadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return default;
            }
            var assessmentResult = await _assessmentService.Assess(request.ToLead());
            return Json(assessmentResult);
        }
    }
}
