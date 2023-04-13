using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Saiketsu.Gateway.Application.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommandValidator : AbstractValidator<CreateVoteCommand>
    {
        public CreateVoteCommandValidator()
        {
            RuleFor(x => x.CandidateId)
                .NotEmpty();

            RuleFor(x => x.ElectionId)
                .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
