using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Queries.GetUser;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUser;

public sealed class GetUserQueryHandlerTests
{
    private readonly Fixture _fixture;

    private readonly GetUserQueryHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<GetUserQuery>> _mockValidator;

    public GetUserQueryHandlerTests()
    {
        _fixture = new Fixture();

        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<GetUserQuery>>();
        _handler = new GetUserQueryHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_user_service_once(string id)
    {
        // Arrange
        var query = new GetUserQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.GetUserAsync(query.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_query_once(string id)
    {
        // Arrange
        var query = new GetUserQuery { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<GetUserQuery>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_valid_user(string id)
    {
        // Arrange
        var query = new GetUserQuery { Id = id };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<UserEntity>();

        _mockUserService.Setup(x => x.GetUserAsync(query.Id)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}