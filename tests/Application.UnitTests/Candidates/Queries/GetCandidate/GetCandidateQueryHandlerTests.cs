using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetCandidateQueryHandler _handler;
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly Mock<IValidator<GetCandidateQuery>> _mockValidator;

    public GetCandidateQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockCandidateService = new Mock<ICandidateService>();
        _mockValidator = new Mock<IValidator<GetCandidateQuery>>();
        _handler = new GetCandidateQueryHandler(_mockCandidateService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_candidate_service_once(int id)
    {
        // Arrange
        var query = new GetCandidateQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockCandidateService.Verify(x => x.GetCandidateAsync(query.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_query_once(int id)
    {
        // Arrange
        var query = new GetCandidateQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(x => x.ValidateAsync(It.IsAny<ValidationContext<GetCandidateQuery>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_candidate(int id)
    {
        // Arrange
        var query = new GetCandidateQuery { Id = id };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Build<CandidateEntity>()
            .With(x => x.Id, id)
            .Create();

        _mockCandidateService.Setup(x => x.GetCandidateAsync(query.Id)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
        Assert.Equal(response!.Id, id);
    }
}