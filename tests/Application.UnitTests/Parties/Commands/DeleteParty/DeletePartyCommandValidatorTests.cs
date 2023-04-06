using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;
using Xunit;

namespace Application.UnitTests.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommandValidatorTests : IDisposable
{
    private readonly DeletePartyCommandValidator _validator;

    public DeletePartyCommandValidatorTests()
    {
        _validator = new DeletePartyCommandValidator();
    }

    public void Dispose()
    {
    }

    [Theory]
    [AutoData]
    public void Should_have_no_errors_when_valid_data(int id)
    {
        // Arrange
        var command = new DeletePartyCommand { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var command = new DeletePartyCommand { Id = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var command = new DeletePartyCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}