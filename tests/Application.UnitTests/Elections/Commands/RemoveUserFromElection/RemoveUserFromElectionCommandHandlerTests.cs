using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Gateway.Application.Interfaces;
using Xunit;

namespace Application.UnitTests.Elections.Commands.RemoveUserFromElection
{
    public sealed class RemoveUserFromElectionCommandHandlerTests
    {
        private readonly RemoveUserFromElectionCommandHandler _handler;
        private readonly Mock<IElectionService> _mockElectionService;
        private readonly Mock<IValidator<RemoveUserFromElectionCommand>> _mockValidator;

        public RemoveUserFromElectionCommandHandlerTests()
        {
            _mockElectionService = new Mock<IElectionService>();
            _mockValidator = new Mock<IValidator<RemoveUserFromElectionCommand>>();
            _handler = new RemoveUserFromElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Should_call_election_service_once()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockElectionService.Verify(x => x.RemoveUserFromElectionAsync(command), Times.Once);
        }

        [Fact]
        public async Task Should_validate_command_once()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockValidator.Verify(
                x => x.ValidateAsync(It.IsAny<ValidationContext<RemoveUserFromElectionCommand>>(), cancellationToken),
                Times.Once());
        }

        [Fact]
        public async Task Should_return_success()
        {
            // Arrange
            var command = new RemoveUserFromElectionCommand();
            var cancellationToken = CancellationToken.None;

            _mockElectionService.Setup(x => x.RemoveUserFromElectionAsync(command)).ReturnsAsync(true);

            // Act
            var response = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(response);
        }
    }
}
