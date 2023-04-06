using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Domain.Enums;
using Xunit;

namespace Application.UnitTests.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandValidatorTests : IDisposable
{
    private readonly CreateElectionCommandValidator _validator;

    public CreateElectionCommandValidatorTests()
    {
        _validator = new CreateElectionCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string name, ElectionType type, string ownerId)
    {
        // Arrange
        var command = new CreateElectionCommand { Name = name, Type = type, OwnerId = ownerId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_name_is_empty(ElectionType type, string ownerId)
    {
        // Arrange
        var command = new CreateElectionCommand { Name = string.Empty, Type = type, OwnerId = ownerId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_name_is_null(ElectionType type, string ownerId)
    {
        // Arrange
        var command = new CreateElectionCommand { Type = type, OwnerId = ownerId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_type_is_null(string name, string ownerId)
    {
        // Arrange
        var command = new CreateElectionCommand { Name = name, OwnerId = ownerId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_type_is_empty(string name, string ownerId)
    {
        // Arrange
        var command = new CreateElectionCommand { Type = default, Name = name, OwnerId = ownerId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_owner_is_null(string name, ElectionType type)
    {
        // Arrange
        var command = new CreateElectionCommand { Name = name, Type = type };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_owner_is_empty(string name, ElectionType type)
    {
        // Arrange
        var command = new CreateElectionCommand { Name = name, Type = type, OwnerId = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId);
    }
}