using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionCandidates
{
    public sealed class GetElectionCandidatesQueryValidatorTests : IDisposable
    {
        private readonly GetElectionCandidatesQueryValidator _validator;

        public GetElectionCandidatesQueryValidatorTests()
        {
            _validator = new GetElectionCandidatesQueryValidator();
        }

        [Theory]
        [AutoData]
        public void Should_have_no_errors_when_valid_data(int electionId)
        {
            // Arrange
            var query = new GetElectionCandidatesQuery { ElectionId = electionId };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_have_error_when_election_id_is_empty()
        {
            // Arrange
            var query = new GetElectionCandidatesQuery { ElectionId = default };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_null()
        {
            // Arrange
            var query = new GetElectionCandidatesQuery();

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
