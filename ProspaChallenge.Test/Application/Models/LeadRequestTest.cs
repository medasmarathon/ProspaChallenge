using FluentAssertions;
using ProspaChallenge.Application.Models;
using ProspaChallenge.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Application.Models
{
    public class LeadRequestTest
    {
        [Fact]
        public void ToLead_ShouldReturnCorrect()
        {
            var request = new LeadRequest
            {
                FirstName = "FN",
                LastName = "LN",
                EmailAddress = "ee@mm",
                PhoneNumber = "123456",
                BusinessNumber = "123789",
                TimeTrading = 10.0F,
                CountryCode = "AU",
                Industry = "Some Industry",
                CitizenshipStatus = CitizenshipStatus.Citizen,
                LoanAmount = 10000.222M
            };

            var result = request.ToLead();

            result.FirstName.Should().Equals(request.FirstName);
            result.LastName.Should().Equals(request.LastName);
            result.EmailAddress.Should().Equals(request.EmailAddress);
            result.PhoneNumber.Should().Equals(request.PhoneNumber);
            result.BusinessNumber.Should().Equals(request.BusinessNumber);
            result.TimeTrading.Should().Equals(request.TimeTrading);
            result.CountryCode.Should().Equals(request.CountryCode);
            result.Industry.Should().Equals(request.Industry);
            result.CitizenshipStatus.Should().Equals(request.CitizenshipStatus);
            result.LoanAmount.Should().Equals(request.LoanAmount);
        }
    }
}
