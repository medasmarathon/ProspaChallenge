namespace ProspaChallenge.Business.Interfaces
{
    // Assuming each Industry is a string for now
    public interface IIndustryRepository
    {
        Task<List<string>> GetAll();
        Task<List<string>> GetAllowedIndustries();
        Task<List<string>> GetBannedIndustries();
    }
}
