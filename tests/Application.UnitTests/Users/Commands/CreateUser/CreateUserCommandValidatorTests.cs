using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Saiketsu.Gateway.Domain.Enums;
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
        const RoleEnum role = RoleEnum.Administrator;
        const string firstName = "First";
        const string lastName = "Last";
        var command = new CreateUserCommand
            { Email = email, Password = password, Role = role, FirstName = firstName, LastName = lastName };

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

    [Fact]
    public void Should_have_error_when_first_name_is_empty()
    {
        // Arrange
        var command = new CreateUserCommand { FirstName = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_have_error_when_first_name_is_null()
    {
        // Arrange
        var command = new CreateUserCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_have_error_when_last_name_is_empty()
    {
        // Arrange
        var command = new CreateUserCommand { LastName = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_have_error_when_last_name_is_null()
    {
        // Arrange
        var command = new CreateUserCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_have_no_error_when_role_is_valid()
    {
        // Arrange
        var command = new CreateUserCommand { Role = RoleEnum.Administrator };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Role);
    }

    [Fact]
    public void Should_have_error_when_role_is_not_valid()
    {
        // Arrange
        var command = new CreateUserCommand { Role = (RoleEnum?)5 };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Role);
    }

    [Fact]
    public void Should_have_no_error_when_role_is_null()
    {
        // Arrange
        var command = new CreateUserCommand { Role = null };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Role);
    }
}