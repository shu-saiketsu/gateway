using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Gateway.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.RemoveCandidateFromElection
{
    public sealed class RemoveCandidateFromElectionCommandHandlerTests
    {
        private readonly RemoveCandidateFromElectionCommandHandler _handler;
        private readonly Mock<IElectionService> _mockElectionService;
        private readonly Mock<IValidator<RemoveCandidateFromElectionCommand>> _mockValidator;

        public RemoveCandidateFromElectionCommandHandlerTests()
        {
            _mockElectionService = new Mock<IElectionService>();
            _mockValidator = new Mock<IValidator<RemoveCandidateFromElectionCommand>>();
            _handler = new RemoveCandidateFromElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Should_call_election_service_once()
        {
            // Arrange
            var command = new RemoveCandidateFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockElectionService.Verify(x => x.RemoveCandidateFromElectionAsync(command), Times.Once);
        }

        [Fact]
        public async Task Should_validate_command_once()
        {
            // Arrange
            var command = new RemoveCandidateFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockValidator.Verify(
                x => x.ValidateAsync(It.IsAny<ValidationContext<RemoveCandidateFromElectionCommand>>(), cancellationToken),
                Times.Once());
        }

        [Fact]
        public async Task Should_return_success()
        {
            // Arrange
            var command = new RemoveCandidateFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            _mockElectionService.Setup(x => x.RemoveCandidateFromElectionAsync(command)).ReturnsAsync(true);

            // Act
            var response = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(response);
        }
    }
}
