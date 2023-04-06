using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Queries.GetElections;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElections;

public sealed class GetElectionsQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetElectionsQueryHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<GetElectionsQuery>> _mockValidator;

    public GetElectionsQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<GetElectionsQuery>>();
        _handler = new GetElectionsQueryHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var query = new GetElectionsQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.GetElectionsAsync(), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetElectionsQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetElectionsQuery>>(), cancellationToken), Times.Once());
    }

    [Fact]
    public async Task Should_return_elections()
    {
        // Arrange
        var query = new GetElectionsQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<ElectionEntity>().ToList();

        _mockElectionService.Setup(x => x.GetElectionsAsync()).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}