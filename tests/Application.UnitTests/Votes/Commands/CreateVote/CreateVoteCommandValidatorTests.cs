using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using Xunit;

namespace Application.UnitTests.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandValidatorTests : IDisposable
    {
        private readonly CreateVoteCommandValidator _validator;

        public CreateVoteCommandValidatorTests()
        {
            _validator = new CreateVoteCommandValidator();
        }

        [Theory]
        [AutoData]
        public void Should_have_no_errors_when_valid_data(int candidateId, int electionId, string userId)
        {
            // Arrange
            var command = new CreateVoteCommand { CandidateId = candidateId, ElectionId = electionId, UserId = userId };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_have_error_when_candidate_id_is_empty()
        {
            // Arrange
            var command = new CreateVoteCommand { CandidateId = default };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CandidateId);
        }

        [Fact]
        public void Should_have_error_when_candidate_id_is_null()
        {
            // Arrange
            var command = new CreateVoteCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CandidateId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_empty()
        {
            // Arrange
            var command = new CreateVoteCommand { ElectionId = default };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_null()
        {
            // Arrange
            var command = new CreateVoteCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_user_id_is_empty()
        {
            // Arrange
            var command = new CreateVoteCommand { UserId = string.Empty };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_have_error_when_user_id_is_null()
        {
            // Arrange
            var command = new CreateVoteCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        public void Dispose()
        {
        }
    }
}
