using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandlerTests
{
    private readonly Fixture _fixture;

    private readonly CreateUserCommandHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<CreateUserCommand>> _mockValidator;

    public CreateUserCommandHandlerTests()
    {
        _fixture = new Fixture();

        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<CreateUserCommand>>();
        _handler = new CreateUserCommandHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_user_service_once(string email, string password)
    {
        // Arrange
        var command = new CreateUserCommand { Email = email, Password = password };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.CreateUserAsync(command), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string email, string password)
    {
        // Arrange
        var command = new CreateUserCommand { Email = email, Password = password };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<CreateUserCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_user_entity(string email, string password)
    {
        // Arrange
        var command = new CreateUserCommand { Email = email, Password = password };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<UserEntity>();

        _mockUserService.Setup(x => x.CreateUserAsync(command)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(methodResponse, response);
    }
}