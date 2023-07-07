using ProspaChallenge.Business.Interfaces;
using ProspaChallenge.Infrastructure.Models;
using System.Text.Json;

namespace ProspaChallenge.Infrastructure.Repositories
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly string _fileLocation;
        public IndustryRepository() 
        {
            _fileLocation = AppContext.BaseDirectory + "./Infrastructure/Json/";
        }
        public async Task<List<string>> GetAll()
        {
            var fileStr = await File.ReadAllTextAsync(_fileLocation + "IndustryList.json");
            var industryList = JsonSerializer.Deserialize<IndustryListModel>(fileStr);
            return industryList.Allowed.Concat(industryList.Banned).ToList();
        }

        public async Task<List<string>> GetAllowedIndustries()
        {
            var fileStr = await File.ReadAllTextAsync(_fileLocation + "IndustryList.json");
            var industryList = JsonSerializer.Deserialize<IndustryListModel>(fileStr);
            return industryList.Allowed;
        }

        public async Task<List<string>> GetBannedIndustries()
        {
            var fileStr = await File.ReadAllTextAsync(_fileLocation + "IndustryList.json");
            var industryList = JsonSerializer.Deserialize<IndustryListModel>(fileStr);
            return industryList.Banned;
        }
    }
}
