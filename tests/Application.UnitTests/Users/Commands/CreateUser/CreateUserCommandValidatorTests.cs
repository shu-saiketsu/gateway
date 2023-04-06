using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidatorTests : IDisposable
{
    private readonly CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
    }

    public void Dispose()
    {
    }

    [Fact]
    public void Should_have_no_errors_when_valid_data()
    {
        // Arrange
        const string email = "email@email.com";
        const string password = "Password100%";
        var command = new CreateUserCommand { Email = email, Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_email_is_empty()
    {
        // Arrange
        const string password = "Password100%";
        var command = new CreateUserCommand { Email = string.Empty, Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_have_error_when_email_is_null()
    {
        // Arrange
        const string password = "Password100%";
        var command = new CreateUserCommand { Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_have_error_when_email_is_invalid()
    {
        // Arrange
        const string email = "fake-email";
        const string password = "Password100%";
        var command = new CreateUserCommand { Email = email, Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_have_error_when_password_is_empty()
    {
        // Arrange
        const string email = "email@email.com";
        var command = new CreateUserCommand { Email = email, Password = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_have_error_when_password_is_null()
    {
        // Arrange
        const string email = "email@email.com";
        var command = new CreateUserCommand { Email = email };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Theory]
    [InlineData("bad-password")]
    [InlineData("BAD-PASSWORD")]
    [InlineData("12345678910")]
    [InlineData("12345678910%")]
    [InlineData("bad-password100%")]
    public void Should_have_error_when_password_is_invalid(string password)
    {
        // Arrange
        const string email = "email@email.com";
        var command = new CreateUserCommand { Email = email, Password = password };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}