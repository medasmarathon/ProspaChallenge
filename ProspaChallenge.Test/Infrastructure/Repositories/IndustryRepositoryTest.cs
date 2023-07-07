using FluentAssertions;
using ProspaChallenge.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProspaChallenge.Test.Infrastructure.Repositories
{
    public class IndustryRepositoryTest
    {
        [Fact]
        public async Task GetAll_Should_ReturnValues()
        {
            var repo = new IndustryRepository();

            var result = await repo.GetAll();

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllowedIndustries_Should_ReturnValues()
        {
            var repo = new IndustryRepository();

            var result = await repo.GetAllowedIndustries();

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetBannedIndustries_Should_ReturnValues()
        {
            var repo = new IndustryRepository();

            var result = await repo.GetBannedIndustries();

            result.Should().NotBeEmpty();
        }
    }
}
