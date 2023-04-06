using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Parties.Queries.GetParty;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Parties.Queries.GetParty;

public sealed class GetPartyQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetPartyQueryHandler _handler;
    private readonly Mock<IPartyService> _mockPartyService;
    private readonly Mock<IValidator<GetPartyQuery>> _mockValidator;

    public GetPartyQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockPartyService = new Mock<IPartyService>();
        _mockValidator = new Mock<IValidator<GetPartyQuery>>();
        _handler = new GetPartyQueryHandler(_mockPartyService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_party_service_once(int id)
    {
        // Arrange
        var query = new GetPartyQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockPartyService.Verify(x => x.GetPartyAsync(query.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_query_once(int id)
    {
        // Arrange
        var query = new GetPartyQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetPartyQuery>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_valid_party(int id)
    {
        // Arrange
        var query = new GetPartyQuery { Id = id };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<PartyEntity>();

        _mockPartyService.Setup(x => x.GetPartyAsync(query.Id)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}