using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionUsers
{
    public sealed class GetElectionUsersQueryValidatorTests : IDisposable
    {
        private readonly GetElectionUsersQueryValidator _validator;

        public GetElectionUsersQueryValidatorTests()
        {
            _validator = new GetElectionUsersQueryValidator();
        }

        [Theory]
        [AutoData]
        public void Should_have_no_errors_when_valid_data(int electionId)
        {
            // Arrange
            var query = new GetElectionUsersQuery { ElectionId = electionId };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_have_error_when_election_id_is_empty()
        {
            // Arrange
            var query = new GetElectionUsersQuery { ElectionId = default };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_null()
        {
            // Arrange
            var query = new GetElectionUsersQuery();

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        public void Dispose()
        {
        }
    }
}
