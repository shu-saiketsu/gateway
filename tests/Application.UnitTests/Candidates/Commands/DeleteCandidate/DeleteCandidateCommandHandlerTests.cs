using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;
using Saiketsu.Gateway.Application.Interfaces;
using Xunit;

namespace Application.UnitTests.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandHandlerTests
{
    private readonly Fixture _fixture;

    private readonly DeleteCandidateCommandHandler _handler;
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly Mock<IValidator<DeleteCandidateCommand>> _mockValidator;

    public DeleteCandidateCommandHandlerTests()
    {
        _fixture = new Fixture();

        _mockCandidateService = new Mock<ICandidateService>();
        _mockValidator = new Mock<IValidator<DeleteCandidateCommand>>();
        _handler = new DeleteCandidateCommandHandler(_mockCandidateService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_candidate_service_once(int id)
    {
        // Arrange
        var command = new DeleteCandidateCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockCandidateService.Verify(x => x.DeleteCandidateAsync(command.Id), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(int id)
    {
        // Arrange
        var command = new DeleteCandidateCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<DeleteCandidateCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_true_when_deleted(int id)
    {
        // Arrange
        var command = new DeleteCandidateCommand { Id = id };
        var cancellationToken = CancellationToken.None;

        _mockCandidateService.Setup(x => x.DeleteCandidateAsync(command.Id)).ReturnsAsync(true);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(response);
    }
}