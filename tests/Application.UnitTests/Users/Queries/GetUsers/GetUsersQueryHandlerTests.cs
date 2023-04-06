using AutoFixture;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Queries.GetUsers;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetUsersQueryHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<GetUsersQuery>> _mockValidator;

    public GetUsersQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<GetUsersQuery>>();
        _handler = new GetUsersQueryHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task Should_call_user_service_once()
    {
        // Arrange
        var query = new GetUsersQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.GetUsersAsync(), Times.Once);
    }

    [Fact]
    public async Task Should_validate_query_once()
    {
        // Arrange
        var query = new GetUsersQuery();
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetUsersQuery>>(), cancellationToken),
            Times.Once());
    }

    [Fact]
    public async Task Should_return_valid_users()
    {
        // Arrange
        var query = new GetUsersQuery();
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.CreateMany<UserEntity>().ToList();

        _mockUserService.Setup(x => x.GetUsersAsync()).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}