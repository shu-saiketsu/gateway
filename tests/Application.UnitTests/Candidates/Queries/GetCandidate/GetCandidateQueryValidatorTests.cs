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
    [InlineData(1)]
    [InlineData(2)]
    public void Should_have_no_errors_when_valid_data(int id)
    {
        // Arrange
        var command = new GetCandidateQuery { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public void Should_have_error_when_id_is_empty_or_null(int id)
    {
        // Arrange
        var command = new GetCandidateQuery { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}