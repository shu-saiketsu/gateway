using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElection;

public sealed class GetElectionQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetElectionQueryHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<GetElectionQuery>> _mockValidator;

    public GetElectionQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<GetElectionQuery>>();
        _handler = new GetElectionQueryHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var query = new GetElectionQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.GetElectionAsync(query.Id), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetElectionQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetElectionQuery>>(), cancellationToken), Times.Once());
    }

    [Fact]
    public async Task Should_return_election()
    {
        // Arrange
        var query = new GetElectionQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<ElectionEntity>();

        _mockElectionService.Setup(x => x.GetElectionAsync(query.Id)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}