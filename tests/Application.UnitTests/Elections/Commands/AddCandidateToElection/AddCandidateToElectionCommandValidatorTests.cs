using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;
using Xunit;

namespace Application.UnitTests.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommandValidatorTests : IDisposable
{
    private readonly AddCandidateToElectionCommandValidator _validator;

    public AddCandidateToElectionCommandValidatorTests()
    {
        _validator = new AddCandidateToElectionCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int electionId, int candidateId)
    {
        // Arrange
        var command = new AddCandidateToElectionCommand { ElectionId = electionId, CandidateId = candidateId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_is_empty(int candidateId)
    {
        // Arrange
        var command = new AddCandidateToElectionCommand { ElectionId = default, CandidateId = candidateId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_election_is_null(int candidateId)
    {
        // Arrange
        var command = new AddCandidateToElectionCommand { CandidateId = candidateId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ElectionId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_candidate_is_empty(int electionId)
    {
        // Arrange
        var command = new AddCandidateToElectionCommand { ElectionId = electionId, CandidateId = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CandidateId);
    }

    [Theory]
    [AutoData]
    public void Should_have_error_when_candidate_is_null(int electionId)
    {
        // Arrange
        var command = new AddCandidateToElectionCommand { ElectionId = electionId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CandidateId);
    }
}