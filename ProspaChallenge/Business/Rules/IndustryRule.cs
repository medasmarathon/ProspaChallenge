using ProspaChallenge.Business.Interfaces;

namespace ProspaChallenge.Business.Rules
{
    public interface IIndustryRule : IAsyncRule<string> { }
    public class IndustryRule : IIndustryRule
    {
        private readonly IIndustryRepository _industryRepository;
        public IndustryRule(IIndustryRepository industryRepository) 
        {
            _industryRepository = industryRepository;
        }
        public async Task<bool> IsQualifiedForAsync(string industry)
        {
            var allowedIndustries = await _industryRepository.GetAllowedIndustries();
            return allowedIndustries.Contains(industry);
        }

        public async Task<bool> IsUnqualifiedForAsync(string industry)
        {
            var bannedIndustries = await _industryRepository.GetBannedIndustries();
            return bannedIndustries.Contains(industry);
        }
    }
}
