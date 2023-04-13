using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using Saiketsu.Gateway.Application.Votes.Queries.CalculateVote;
using Xunit;

namespace Application.UnitTests.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQueryValidatorTests : IDisposable
    {
        private readonly CalculateVoteQueryValidator _validator;

        public CalculateVoteQueryValidatorTests()
        {
            _validator = new CalculateVoteQueryValidator();
        }

        [Theory]
        [AutoData]
        public void Should_have_no_errors_when_valid_data(int electionId)
        {
            // Arrange
            var command = new CalculateVoteQuery { ElectionId = electionId };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_have_error_when_election_id_is_empty()
        {
            // Arrange
            var query = new CalculateVoteQuery { ElectionId = default };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_null()
        {
            // Arrange
            var query = new CalculateVoteQuery();

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
