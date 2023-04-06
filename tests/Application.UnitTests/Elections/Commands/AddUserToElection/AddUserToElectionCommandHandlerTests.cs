using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Interfaces;
using Xunit;

namespace Application.UnitTests.Elections.Commands.AddUserToElection;

public sealed class AddUserToElectionCommandHandlerTests
{
    private readonly AddUserToElectionCommandHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<AddUserToElectionCommand>> _mockValidator;

    public AddUserToElectionCommandHandlerTests()
    {
        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<AddUserToElectionCommand>>();
        _handler = new AddUserToElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var command = new AddUserToElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.AddUserToElectionAsync(command), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var command = new AddUserToElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<AddUserToElectionCommand>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_success()
    {
        // Arrange
        var command = new AddUserToElectionCommand();
        var cancellationToken = CancellationToken.None;

        _mockElectionService.Setup(x => x.AddUserToElectionAsync(command)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}