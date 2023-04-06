using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Users.Queries.GetUser;
using Xunit;

namespace Application.UnitTests.Users.Queries.GetUser;

public sealed class GetUserQueryValidatorTests : IDisposable
{
    private readonly GetUserQueryValidator _validator;

    public GetUserQueryValidatorTests()
    {
        _validator = new GetUserQueryValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(string id)
    {
        // Arrange
        var query = new GetUserQuery { Id = id };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var query = new GetUserQuery { Id = string.Empty };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var query = new GetUserQuery();

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}