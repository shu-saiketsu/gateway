using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Parties.Commands.CreateParty;
using Xunit;

namespace Application.UnitTests.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandValidatorTests : IDisposable
{
    private readonly CreatePartyCommandValidator _validator;

    public CreatePartyCommandValidatorTests()
    {
        _validator = new CreatePartyCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string name, string description)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name, Description = description };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_name_is_empty(string description)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = string.Empty, Description = description };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_name_is_null(string description)
    {
        // Arrange
        var command = new CreatePartyCommand { Description = description };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_description_is_empty(string name)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name, Description = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_description_is_null(string name)
    {
        // Arrange
        var command = new CreatePartyCommand { Name = name };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}