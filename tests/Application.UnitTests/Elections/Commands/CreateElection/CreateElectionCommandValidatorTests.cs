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
    public void Should_have_no_errors_when_valid_data(string name, ElectionType type, string ownerId,
        DateTime startDate, DateTime endDate)
    {
        // Arrange
        var command = new CreateElectionCommand
            { Name = name, Type = type, OwnerId = ownerId, StartDate = startDate, EndDate = endDate };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_name_is_empty()
    {
        // Arrange
        var command = new CreateElectionCommand { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_have_error_when_name_is_null()
    {
        // Arrange
        var command = new CreateElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_have_error_when_election_type_is_null()
    {
        // Arrange
        var command = new CreateElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public void Should_have_error_when_election_type_is_empty()
    {
        // Arrange
        var command = new CreateElectionCommand { Type = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public void Should_have_error_when_owner_is_null()
    {
        // Arrange
        var command = new CreateElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId);
    }

    [Fact]
    public void Should_have_error_when_owner_is_empty()
    {
        // Arrange
        var command = new CreateElectionCommand { OwnerId = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OwnerId);
    }

    [Fact]
    public void Should_have_error_when_start_date_is_null()
    {
        // Arrange
        var command = new CreateElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.StartDate);
    }

    [Fact]
    public void Should_have_error_when_start_date_is_empty()
    {
        // Arrange
        var command = new CreateElectionCommand { StartDate = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.StartDate);
    }

    [Fact]
    public void Should_have_error_when_end_date_is_null()
    {
        // Arrange
        var command = new CreateElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EndDate);
    }

    [Fact]
    public void Should_have_error_when_end_date_is_empty()
    {
        // Arrange
        var command = new CreateElectionCommand { EndDate = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EndDate);
    }
}