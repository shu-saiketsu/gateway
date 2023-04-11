using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetElectionUsersQueryHandler _handler;
    private readonly Mock<IElectionService> _mockElectionService;
    private readonly Mock<IValidator<GetElectionUsersQuery>> _mockValidator;

    public GetElectionUsersQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockElectionService = new Mock<IElectionService>();
        _mockValidator = new Mock<IValidator<GetElectionUsersQuery>>();
        _handler = new GetElectionUsersQueryHandler(_mockElectionService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_election_service_once()
    {
        // Arrange
        var query = new GetElectionUsersQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockElectionService.Verify(x => x.GetElectionUsersAsync(query.ElectionId), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetElectionUsersQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetElectionUsersQuery>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_election()
    {
        // Arrange
        var query = new GetElectionUsersQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<UserEntity>().ToList();

        _mockElectionService.Setup(x => x.GetElectionUsersAsync(query.ElectionId)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}