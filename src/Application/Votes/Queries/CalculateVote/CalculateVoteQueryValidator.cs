using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Saiketsu.Gateway.Application.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQueryValidator : AbstractValidator<CalculateVoteQuery>
    {
        public CalculateVoteQueryValidator()
        {
            RuleFor(x => x.ElectionId)
                .NotEmpty();
        }
    }
}
