using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQueryHandler : IRequestHandler<CalculateVoteQuery, Dictionary<int, int>?>
    {
        private readonly IVoteService _voteService;
        private readonly IValidator<CalculateVoteQuery> _validator;

        public CalculateVoteQueryHandler(IVoteService voteService, IValidator<CalculateVoteQuery> validator)
        {
            _voteService = voteService;
            _validator = validator;
        }

        public async Task<Dictionary<int, int>?> Handle(CalculateVoteQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var result = await _voteService.CalculateVoteAsync(request.ElectionId);

            return result;
        }
    }
}
