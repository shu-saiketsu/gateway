using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.BlockUser;

public sealed class BlockUserCommandValidatorTests : IDisposable
{
    private readonly BlockUserCommandValidator _validator;

    public BlockUserCommandValidatorTests()
    {
        _validator = new BlockUserCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string id)
    {
        // Arrange
        var command = new BlockUserCommand { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var command = new BlockUserCommand { Id = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var command = new BlockUserCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}