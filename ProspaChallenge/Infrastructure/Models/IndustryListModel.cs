namespace ProspaChallenge.Infrastructure.Models
{
    public class IndustryListModel
    {
        public List<string> Banned { get; set; } = new();
        public List<string> Allowed { get; set; } = new();
    }
}
