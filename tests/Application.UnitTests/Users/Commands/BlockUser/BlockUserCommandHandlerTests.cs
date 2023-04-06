using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.BlockUser;

public sealed class BlockUserCommandHandlerTests
{
    private readonly BlockUserCommandHandler _handler;
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IValidator<BlockUserCommand>> _mockValidator;

    public BlockUserCommandHandlerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockValidator = new Mock<IValidator<BlockUserCommand>>();
        _handler = new BlockUserCommandHandler(_mockUserService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_user_service_once(string id)
    {
        // Arrange
        var command = new BlockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockUserService.Verify(x => x.BlockUserAsync(command), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string id)
    {
        // Arrange
        var command = new BlockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<BlockUserCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_success(string id)
    {
        // Arrange
        var command = new BlockUserCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        _mockUserService.Setup(x => x.BlockUserAsync(command)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}