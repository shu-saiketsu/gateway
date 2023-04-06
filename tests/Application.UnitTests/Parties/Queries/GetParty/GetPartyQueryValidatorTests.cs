using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Parties.Queries.GetParty;
using Xunit;

namespace Application.UnitTests.Parties.Queries.GetParty;

public sealed class GetPartyQueryValidatorTests : IDisposable
{
    private readonly GetPartyQueryValidator _validator;

    public GetPartyQueryValidatorTests()
    {
        _validator = new GetPartyQueryValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int id)
    {
        // Arrange
        var query = new GetPartyQuery { Id = id };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var query = new GetPartyQuery { Id = default };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var query = new GetPartyQuery();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}