using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using AutoFixture.Xunit2;
using Xunit;

namespace Application.UnitTests.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandHandlerTests
    {
        private readonly CreateVoteCommandHandler _handler;
        private readonly Mock<IVoteService> _mockVoteService;
        private readonly Mock<IValidator<CreateVoteCommand>> _mockValidator;

        public CreateVoteCommandHandlerTests()
        {
            _mockVoteService = new Mock<IVoteService>();
            _mockValidator = new Mock<IValidator<CreateVoteCommand>>();
            _handler = new CreateVoteCommandHandler(_mockVoteService.Object, _mockValidator.Object);
        }

        [Theory]
        [AutoData]
        public async Task Should_call_vote_service_once(CreateVoteCommand command)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockVoteService.Verify(x => x.CastVoteAsync(command), Times.Once);
        }

        [Fact]
        public async Task Should_validate_command_once()
        {
            // Arrange
            var command = new CreateVoteCommand();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _mockValidator.Verify(
                x => x.ValidateAsync(It.IsAny<ValidationContext<CreateVoteCommand>>(), cancellationToken),
                Times.Once());
        }

        [Fact]
        public async Task Should_return_success()
        {
            // Arrange
            var command = new CreateVoteCommand();
            var cancellationToken = CancellationToken.None;

            _mockVoteService.Setup(x => x.CastVoteAsync(command)).ReturnsAsync(true);

            // Act
            var response = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(response);
        }
    }
}
