using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.AddUserToElection;

public sealed class AddUserToElectionCommandValidatorTests : IDisposable
{
    private readonly AddUserToElectionCommandValidator _validator;

    public AddUserToElectionCommandValidatorTests()
    {
        _validator = new AddUserToElectionCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int electionId, string userId)
    {
        // Arrange
        var command = new AddUserToElectionCommand { ElectionId = electionId, UserId = userId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_is_empty(string userId)
    {
        // Arrange
        var command = new AddUserToElectionCommand { ElectionId = default, UserId = userId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_is_null(string userId)
    {
        // Arrange
        var command = new AddUserToElectionCommand { UserId = userId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_user_is_empty(int electionId)
    {
        // Arrange
        var command = new AddUserToElectionCommand { ElectionId = electionId, UserId = string.Empty };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_user_is_null(int electionId)
    {
        // Arrange
        var command = new AddUserToElectionCommand { ElectionId = electionId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}