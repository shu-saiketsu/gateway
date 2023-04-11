using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;
using Xunit;

namespace Application.UnitTests.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQueryValidatorTests : IDisposable
{
    private readonly GetElectionsForUserQueryValidator _validator;

    public GetElectionsForUserQueryValidatorTests()
    {
        _validator = new GetElectionsForUserQueryValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string userId, bool eligible)
    {
        // Arrange
        var query = new GetElectionsForUserQuery { UserId = userId, Eligible = eligible };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_no_errors_when_eligible_is_true()
    {
        // Arrange
        var query = new GetElectionsForUserQuery { Eligible = true };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Eligible);
    }

    [Fact]
    public void Should_have_no_errors_when_eligible_is_false()
    {
        // Arrange
        var query = new GetElectionsForUserQuery { Eligible = false };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Eligible);
    }

    [Fact]
    public void Should_have_no_errors_when_eligible_is_default()
    {
        // Arrange
        var query = new GetElectionsForUserQuery { Eligible = default };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Eligible);
    }

    [Fact]
    public void Should_have_error_when_user_id_is_empty()
    {
        // Arrange
        var query = new GetElectionsForUserQuery { UserId = string.Empty };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_have_error_when_user_id_is_null()
    {
        // Arrange
        var query = new GetElectionsForUserQuery();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}