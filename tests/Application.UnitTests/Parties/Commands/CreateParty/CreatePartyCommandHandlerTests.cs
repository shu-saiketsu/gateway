using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Parties.Commands.CreateParty;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandHandlerTests
{
    private readonly Fixture _fixture;

    private readonly CreatePartyCommandHandler _handler;
    private readonly Mock<IPartyService> _mockPartyService;
    private readonly Mock<IValidator<CreatePartyCommand>> _mockValidator;

    public CreatePartyCommandHandlerTests()
    {
        _fixture = new Fixture();

        _mockPartyService = new Mock<IPartyService>();
        _mockValidator = new Mock<IValidator<CreatePartyCommand>>();
        _handler = new CreatePartyCommandHandler(_mockPartyService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_party_service_once(string name, string description)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name, Description = description };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockPartyService.Verify(x => x.CreatePartyAsync(command), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string name, string description)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name, Description = description };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<CreatePartyCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_valid_party(string name, string partyId)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name, Description = partyId };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<PartyEntity>();

        _mockPartyService.Setup(x => x.CreatePartyAsync(command)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}