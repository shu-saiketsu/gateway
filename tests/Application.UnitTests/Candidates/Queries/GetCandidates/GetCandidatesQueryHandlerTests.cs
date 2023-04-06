using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Candidates.Queries.GetCandidates;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Candidates.Queries.GetCandidates;

public sealed class GetCandidatesQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetCandidatesQueryHandler _handler;
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly Mock<IValidator<GetCandidatesQuery>> _mockValidator;

    public GetCandidatesQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockCandidateService = new Mock<ICandidateService>();
        _mockValidator = new Mock<IValidator<GetCandidatesQuery>>();
        _handler = new GetCandidatesQueryHandler(_mockCandidateService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_candidate_service_once()
    {
        // Arrange
        var command = new GetCandidatesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockCandidateService.Verify(x => x.GetCandidatesAsync(), Times.Once);
    }

    [Fact]
    public async Task Should_validate_command_once()
    {
        // Arrange
        var command = new GetCandidatesQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetCandidatesQuery>>(), cancellationToken), Times.Once());
    }

    [Fact]
    public async Task Should_return_candidate_list()
    {
        // Arrange
        var command = new GetCandidatesQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<CandidateEntity>().ToList();

        _mockCandidateService.Setup(x => x.GetCandidatesAsync()).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotEmpty(response);
    }
}