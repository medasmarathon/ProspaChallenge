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

            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.EmailAddress.Should().Be(request.EmailAddress);
            result.PhoneNumber.Should().Be(request.PhoneNumber);
            result.BusinessNumber.Should().Be(request.BusinessNumber);
            result.TimeTrading.Should().Be(request.TimeTrading);
            result.CountryCode.Should().Be(request.CountryCode);
            result.Industry.Should().Be(request.Industry);
            result.CitizenshipStatus.Should().Be(request.CitizenshipStatus);
            result.LoanAmount.Should().Be(request.LoanAmount);
        }
    }
}
