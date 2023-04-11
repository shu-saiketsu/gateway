using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetElectionsForUserQueryHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<GetElectionsForUserQuery>> _mockValidator;

    public GetElectionsForUserQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<GetElectionsForUserQuery>>();
        _handler = new GetElectionsForUserQueryHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var query = new GetElectionsForUserQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.GetElectionsForUserAsync(query), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetElectionsForUserQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetElectionsForUserQuery>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_candidates()
    {
        // Arrange
        var query = new GetElectionsForUserQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<ElectionEntity>().ToList();

        _mockElectionService.Setup(x => x.GetElectionsForUserAsync(query)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}