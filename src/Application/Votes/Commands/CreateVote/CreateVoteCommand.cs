using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Gateway.Application.Votes.Commands.CreateVote
{
    public sealed class CreateVoteCommand : IRequest<bool>
    {
        public int ElectionId { get; set; }
        public int CandidateId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
