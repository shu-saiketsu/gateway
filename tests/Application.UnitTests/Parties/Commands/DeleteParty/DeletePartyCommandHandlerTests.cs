using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;
using Xunit;

namespace Application.UnitTests.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommandHandlerTests
{
    private readonly DeletePartyCommandHandler _handler;
    private readonly Mock<IPartyService> _mockPartyService;
    private readonly Mock<IValidator<DeletePartyCommand>> _mockValidator;

    public DeletePartyCommandHandlerTests()
    {
        _mockPartyService = new Mock<IPartyService>();
        _mockValidator = new Mock<IValidator<DeletePartyCommand>>();
        _handler = new DeletePartyCommandHandler(_mockPartyService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_party_service_once(int id)
    {
        // Arrange
        var command = new DeletePartyCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockPartyService.Verify(x => x.DeletePartyAsync(command.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(int id)
    {
        // Arrange
        var command = new DeletePartyCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<DeletePartyCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_valid_success(int id)
    {
        // Arrange
        var command = new DeletePartyCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        _mockPartyService.Setup(x => x.DeletePartyAsync(command.Id)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}