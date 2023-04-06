using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.UnblockUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.UnblockUser;

public sealed class UnblockUserCommandHandlerTests
{
    private readonly UnblockUserCommandHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<UnblockUserCommand>> _mockValidator;

    public UnblockUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<UnblockUserCommand>>();
        _handler = new UnblockUserCommandHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_user_service_once(string id)
    {
        // Arrange
        var command = new UnblockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.UnblockUserAsync(command), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string id)
    {
        // Arrange
        var command = new UnblockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<UnblockUserCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_success(string id)
    {
        // Arrange
        var command = new UnblockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        _mockUserService.Setup(x => x.UnblockUserAsync(command)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}