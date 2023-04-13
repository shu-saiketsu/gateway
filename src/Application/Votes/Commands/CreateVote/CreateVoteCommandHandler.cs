using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, bool>
    {
        private readonly IVoteService _voteService;
        private readonly IValidator<CreateVoteCommand> _validator;

        public CreateVoteCommandHandler(IVoteService voteService, IValidator<CreateVoteCommand> validator)
        {
            _voteService = voteService;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var success = await _voteService.CastVoteAsync(request);

            return success;
        }
    }
}
