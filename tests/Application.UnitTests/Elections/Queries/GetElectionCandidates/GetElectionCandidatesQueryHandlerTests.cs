using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionCandidates;

public sealed class GetElectionCandidatesQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetElectionCandidatesQueryHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<GetElectionCandidatesQuery>> _mockValidator;

    public GetElectionCandidatesQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<GetElectionCandidatesQuery>>();
        _handler = new GetElectionCandidatesQueryHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var query = new GetElectionCandidatesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.GetElectionCandidatesAsync(query.ElectionId), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetElectionCandidatesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetElectionCandidatesQuery>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_candidates()
    {
        // Arrange
        var query = new GetElectionCandidatesQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<CandidateEntity>().ToList();

        _mockElectionService.Setup(x => x.GetElectionCandidatesAsync(query.ElectionId)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}