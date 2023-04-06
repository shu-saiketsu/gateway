using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Commands.UnblockUser;
using Xunit;

namespace Application.UnitTests.Users.Commands.UnblockUser;

public sealed class UnblockUserCommandValidatorTests : IDisposable
{
    private readonly UnblockUserCommandValidator _validator;

    public UnblockUserCommandValidatorTests()
    {
        _validator = new UnblockUserCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string id)
    {
        // Arrange
        var command = new UnblockUserCommand { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var command = new UnblockUserCommand { Id = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var command = new UnblockUserCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}