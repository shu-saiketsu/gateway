using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.DeleteUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandlerTests
{
    private readonly DeleteUserCommandHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<DeleteUserCommand>> _mockValidator;

    public DeleteUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<DeleteUserCommand>>();
        _handler = new DeleteUserCommandHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_user_service_once(string id)
    {
        // Arrange
        var command = new DeleteUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.DeleteUserAsync(command.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string id)
    {
        // Arrange
        var command = new DeleteUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<DeleteUserCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_success(string id)
    {
        // Arrange
        var command = new DeleteUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        _mockUserService.Setup(x => x.DeleteUserAsync(command.Id)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}