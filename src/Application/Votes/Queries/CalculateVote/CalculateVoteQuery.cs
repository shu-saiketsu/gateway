using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Saiketsu.Gateway.Application.Votes.Queries.CalculateVote
{
    public sealed class CalculateVoteQuery : IRequest<Dictionary<int, int>?>
    {
        public int ElectionId { get; set; }
    }
}
