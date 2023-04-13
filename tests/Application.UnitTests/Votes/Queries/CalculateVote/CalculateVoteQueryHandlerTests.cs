using FluentValidation;
using Moq;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Saiketsu.Gateway.Application.Votes.Queries.CalculateVote;
using AutoFixture.Xunit2;
using Xunit;

namespace Application.UnitTests.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQueryHandlerTests
    {
        private readonly Fixture _fixture;

        private readonly CalculateVoteQueryHandler _handler;
        private readonly Mock<IVoteService> _mockVoteService;
        private readonly Mock<IValidator<CalculateVoteQuery>> _mockValidator;

        public CalculateVoteQueryHandlerTests()
        {
            _fixture = new Fixture();

            _mockVoteService = new Mock<IVoteService>();
            _mockValidator = new Mock<IValidator<CalculateVoteQuery>>();
            _handler = new CalculateVoteQueryHandler(_mockVoteService.Object, _mockValidator.Object);
        }

        [Theory]
        [AutoData]
        public async Task Should_call_vote_service_once(CalculateVoteQuery query)
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(query, cancellationToken);

            // Assert
            _mockVoteService.Verify(x => x.CalculateVoteAsync(query.ElectionId), Times.Once);
        }

        [Fact]
        public async Task Should_validate_query_once()
        {
            // Arrange
            var query = new CalculateVoteQuery();
            var cancellationToken = CancellationToken.None;

            // Act
            await _handler.Handle(query, cancellationToken);

            // Assert
            _mockValidator.Verify(
                x => x.ValidateAsync(It.IsAny<ValidationContext<CalculateVoteQuery>>(), cancellationToken),
                Times.Once());
        }

        [Fact]
        public async Task Should_return_dictionary()
        {
            // Arrange
            var query = new CalculateVoteQuery();
            var cancellationToken = CancellationToken.None;
            var methodResponse = _fixture.Create<Dictionary<int, int>>();

            _mockVoteService.Setup(x => x.CalculateVoteAsync(query.ElectionId)).ReturnsAsync(methodResponse);

            // Act
            var response = await _handler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(response, methodResponse);
        }
    }
}
