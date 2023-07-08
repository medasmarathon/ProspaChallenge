using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using ProspaChallenge.Application.Models;
using ProspaChallenge.Business.Models;
using ProspaChallenge.Services;

namespace ProspaChallenge.Controllers
{
    [ApiController]
    public class LeadController : Controller
    {
        private readonly IAssessmentService _assessmentService;
        private readonly IMemoryCache _cache;
        public LeadController(IAssessmentService assessmentService, IMemoryCache cache) 
        {
            _assessmentService = assessmentService;
            _cache = cache;
        }
        [HttpPost]
        [Route("api/lead/assessment")]
        public async Task<JsonResult> Assessment(LeadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return default;
            }

            if (!_cache.TryGetValue(Request.RouteValues["RequestBodyHashCode"] ?? "", out var assessmentResult))
            {
                assessmentResult = await _assessmentService.Assess(request.ToLead());

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _cache.Set(Request.RouteValues["RequestBodyHashCode"], assessmentResult, cacheEntryOptions);
            }

            return Json(assessmentResult);
        }

    }

}
