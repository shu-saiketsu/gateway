using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection
{
    public sealed class AddUserToElectionCommandHandler : IRequestHandler<AddUserToElectionCommand, bool>
    {
        private readonly IElectionService _electionService;
        private readonly IValidator<AddUserToElectionCommand> _validator;

        public AddUserToElectionCommandHandler(IElectionService electionService, IValidator<AddUserToElectionCommand> validator)
        {
            _electionService = electionService;
            _validator = validator;
        }

        public async Task<bool> Handle(AddUserToElectionCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var result = await _electionService.AddUserToElectionAsync(request);

            return result;
        }
    }
}
