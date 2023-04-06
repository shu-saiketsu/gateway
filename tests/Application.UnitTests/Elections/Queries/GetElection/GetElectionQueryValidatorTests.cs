using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElection;

public sealed class GetElectionQueryValidatorTests : IDisposable
{
    private readonly GetElectionQueryValidator _validator;

    public GetElectionQueryValidatorTests()
    {
        _validator = new GetElectionQueryValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int electionId)
    {
        // Arrange
        var query = new GetElectionQuery { Id = electionId };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var query = new GetElectionQuery { Id = default };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var query = new GetElectionQuery();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}