using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.RemoveCandidateFromElection;

public sealed class RemoveCandidateFromElectionCommandValidatorTests : IDisposable
{
    private readonly RemoveCandidateFromElectionCommandValidator _validator;

    public RemoveCandidateFromElectionCommandValidatorTests()
    {
        _validator = new RemoveCandidateFromElectionCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int candidateId, int electionId)
    {
        // Arrange
        var command = new RemoveCandidateFromElectionCommand { CandidateId = candidateId, ElectionId = electionId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_election_id_is_empty()
    {
        // Arrange
        var command = new RemoveCandidateFromElectionCommand { ElectionId = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Fact]
    public void Should_have_error_when_election_id_is_null()
    {
        // Arrange
        var command = new RemoveCandidateFromElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Fact]
    public void Should_have_error_when_candidate_id_is_empty()
    {
        // Arrange
        var command = new RemoveCandidateFromElectionCommand { CandidateId = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CandidateId);
    }

    [Fact]
    public void Should_have_error_when_candidate_id_is_null()
    {
        // Arrange
        var command = new RemoveCandidateFromElectionCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CandidateId);
    }
}