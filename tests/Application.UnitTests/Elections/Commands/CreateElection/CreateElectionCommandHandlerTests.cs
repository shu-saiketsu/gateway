using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;
using Xunit;

namespace Application.UnitTests.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandHandlerTests
{
    private readonly Fixture _fixture;

    private readonly CreateElectionCommandHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<CreateElectionCommand>> _mockValidator;

    public CreateElectionCommandHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<CreateElectionCommand>>();
        _handler = new CreateElectionCommandHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var command = new CreateElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.CreateElectionAsync(command), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var command = new CreateElectionCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<CreateElectionCommand>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_election()
    {
        // Arrange
        var command = new CreateElectionCommand();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<ElectionEntity>();

        _mockElectionService.Setup(x => x.CreateElectionAsync(command)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}