using System.Runtime.Serialization;

namespace ProspaChallenge.Business.Models
{
    public class Lead
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string BusinessNumber { get; set; } = string.Empty;
        public decimal LoanAmount { get; set; } = new();
        public CitizenshipStatus CitizenshipStatus { get; set; }
        public float TimeTrading { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
    }

    public enum CitizenshipStatus
    {
        Citizen,
        [EnumMember(Value = "Permanent Resident")]
        PermanentResident
    }
}
