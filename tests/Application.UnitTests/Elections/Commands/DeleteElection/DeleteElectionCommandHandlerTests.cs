using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;
using Saiketsu.Gateway.Application.Interfaces;
using Xunit;

namespace Application.UnitTests.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandHandlerTests
{
    private readonly DeleteElectionCommandHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<DeleteElectionCommand>> _mockValidator;

    public DeleteElectionCommandHandlerTests()
    {
        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<DeleteElectionCommand>>();
        _handler = new DeleteElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var command = new DeleteElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.DeleteElectionAsync(command.Id), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var command = new DeleteElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<DeleteElectionCommand>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_success()
    {
        // Arrange
        var command = new DeleteElectionCommand();
        var cancellationToken = CancellationToken.None;

        _mockElectionService.Setup(x => x.DeleteElectionAsync(command.Id)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}