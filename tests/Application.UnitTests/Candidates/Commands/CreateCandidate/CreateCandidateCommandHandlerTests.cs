using AutoFixture;
using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandHandlerTests
{
    private readonly Fixture _fixture;

    private readonly CreateCandidateCommandHandler _handler;
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly Mock<IValidator<CreateCandidateCommand>> _mockValidator;

    public CreateCandidateCommandHandlerTests()
    {
        _fixture = new Fixture();

        _mockCandidateService = new Mock<ICandidateService>();
        _mockValidator = new Mock<IValidator<CreateCandidateCommand>>();
        _handler = new CreateCandidateCommandHandler(_mockCandidateService.Object, _mockValidator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Should_call_candidate_service_once(string name, int partyId)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name, PartyId = partyId };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockCandidateService.Verify(x => x.CreateCandidateAsync(command), Times.Once);
    }

    [Theory]
    [AutoData]
    public async Task Should_validate_command_once(string name, int partyId)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name, PartyId = partyId };
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        _mockValidator.Verify(
            x => x.ValidateAsync(It.IsAny<ValidationContext<CreateCandidateCommand>>(), cancellationToken),
            Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Should_return_valid_candidate(string name, int partyId)
    {
        // Arrange
        var command = new CreateCandidateCommand { Name = name, PartyId = partyId };
        var cancellationToken = CancellationToken.None;
        var methodResponse = _fixture.Create<CandidateEntity>();

        _mockCandidateService.Setup(x => x.CreateCandidateAsync(command)).ReturnsAsync(methodResponse);

        // Act
        var response = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.Equal(response, methodResponse);
    }
}