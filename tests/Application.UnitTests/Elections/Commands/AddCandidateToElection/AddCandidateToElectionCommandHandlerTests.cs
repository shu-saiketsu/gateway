using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;
using Saiketsu.Gateway.Application.Interfaces;
using Xunit;

namespace Application.UnitTests.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommandHandlerTests
{
    private readonly AddCandidateToElectionCommandHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<AddCandidateToElectionCommand>> _mockValidator;

    public AddCandidateToElectionCommandHandlerTests()
    {
        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<AddCandidateToElectionCommand>>();
        _handler = new AddCandidateToElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var command = new AddCandidateToElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.AddCandidateToElectionAsync(command), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var command = new AddCandidateToElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<AddCandidateToElectionCommand>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_success()
    {
        // Arrange
        var command = new AddCandidateToElectionCommand();
        var cancellationToken = CancellationToken.None;

        _mockElectionService.Setup(x => x.AddCandidateToElectionAsync(command)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}