using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandValidatorTests : IDisposable
{
    private readonly DeleteElectionCommandValidator _validator;

    public DeleteElectionCommandValidatorTests()
    {
        _validator = new DeleteElectionCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int electionId)
    {
        // Arrange
        var command = new DeleteElectionCommand { Id = electionId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_election_is_empty()
    {
        // Arrange
        var command = new DeleteElectionCommand { Id = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_election_is_null()
    {
        // Arrange
        var command = new DeleteElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}