using FluentValidation.TestHelper;
using Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;
using Xunit;

namespace Application.UnitTests.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandValidatorTests : IDisposable
{
    private readonly DeleteCandidateCommandValidator _validator;

    public DeleteCandidateCommandValidatorTests()
    {
        _validator = new DeleteCandidateCommandValidator();
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
        var command = new DeleteCandidateCommand { Id = id };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_have_error_when_id_is_empty()
    {
        // Arrange
        var command = new DeleteCandidateCommand { Id = default };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_have_error_when_id_is_null()
    {
        // Arrange
        var command = new DeleteCandidateCommand();

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }
}