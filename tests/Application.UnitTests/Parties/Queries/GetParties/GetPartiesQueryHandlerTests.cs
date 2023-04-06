using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Parties.Queries.GetParties;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Parties.Queries.GetParties;

public sealed class GetPartiesQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetPartiesQueryHandler _handler;
    private readonly Mock<IPartyService> _mockPartyService;
    private readonly Mock<IValidator<GetPartiesQuery>> _mockValidator;

    public GetPartiesQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockPartyService = new Mock<IPartyService>();
        _mockValidator = new Mock<IValidator<GetPartiesQuery>>();
        _handler = new GetPartiesQueryHandler(_mockPartyService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_party_service_once()
    {
        // Arrange
        var query = new GetPartiesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockPartyService.Verify(x => x.GetPartiesAsync(), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var query = new GetPartiesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetPartiesQuery>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_valid_parties()
    {
        // Arrange
        var query = new GetPartiesQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<PartyEntity>().ToList();

        _mockPartyService.Setup(x => x.GetPartiesAsync()).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}