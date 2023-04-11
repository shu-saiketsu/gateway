using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.RemoveUserFromElection
{
    public sealed class RemoveUserFromElectionCommandValidatorTests : IDisposable
    {
        private readonly RemoveUserFromElectionCommandValidator _validator;

        public RemoveUserFromElectionCommandValidatorTests()
        {
            _validator = new RemoveUserFromElectionCommandValidator();
        }

        [Theory]
        [AutoData]
        public void Should_have_no_errors_when_valid_data(string userId, int electionId)
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand { UserId = userId, ElectionId = electionId };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_have_error_when_election_id_is_empty()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand { ElectionId = default };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_election_id_is_null()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ElectionId);
        }

        [Fact]
        public void Should_have_error_when_user_id_is_empty()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand { UserId = string.Empty };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_have_error_when_user_id_is_null()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand();

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
