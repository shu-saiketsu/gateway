using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.DeleteUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandValidatorTests : IDisposable
{
    private readonly DeleteUserCommandValidator _validator;

    public DeleteUserCommandValidatorTests()
    {
        _validator = new DeleteUserCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string id)
    {
        // Arrange
        var command = new DeleteUserCommand { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var command = new DeleteUserCommand { Id = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var command = new DeleteUserCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}