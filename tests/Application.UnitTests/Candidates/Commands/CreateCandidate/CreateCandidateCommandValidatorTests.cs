using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;
using Xunit;

namespace Application.UnitTests.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandValidatorTests : IDisposable
{
    private readonly CreateCandidateCommandValidator _validator;

    public CreateCandidateCommandValidatorTests()
    {
        _validator = new CreateCandidateCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [InlineData("Name", 1)]
    [InlineData("Name", null)]
    public void Should_have_no_errors_when_valid_data(string name, int? partyId)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name, PartyId = partyId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("", null)]
    [InlineData(null, null)]
    public void Should_have_error_when_name_is_empty_or_null(string? name, int? partyId)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name!, PartyId = partyId };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [AutoData]
    public void Should_have_no_error_when_party_is_null(string name)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name, PartyId = null };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.PartyId);
    }
}