using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;
using Xunit;

namespace Application.UnitTests.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQueryValidatorTests : IDisposable
{
    private readonly GetCandidateQueryValidator _validator;

    public GetCandidateQueryValidatorTests()
    {
        _validator = new GetCandidateQueryValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int id)
    {
        // Arrange
        var query = new GetCandidateQuery { Id = id };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var query = new GetCandidateQuery { Id = default };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var query = new GetCandidateQuery();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}