using ProspaChallenge.Application.Converters;
using ProspaChallenge.Business.Models;
using System.Text.Json.Serialization;

namespace ProspaChallenge.Application.Models
{
    public class LeadRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string BusinessNumber { get; set; } = string.Empty;
        [JsonConverter(typeof(CurrencyConverter))]
        public decimal LoanAmount { get; set; } = new();
        public CitizenshipStatus CitizenshipStatus { get; set; }
        public float TimeTrading { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;

        public Lead ToLead()
        {
            return new Lead
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                PhoneNumber = PhoneNumber,
                BusinessNumber = BusinessNumber,
                TimeTrading = TimeTrading,
                CountryCode = CountryCode,
                Industry = Industry,
            };
        }
    }
}
