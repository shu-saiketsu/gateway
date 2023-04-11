using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQueryHandler : IRequestHandler<GetElectionUsersQuery, List<UserEntity>?>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<GetElectionUsersQuery> _validator;

    public GetElectionUsersQueryHandler(IElectionService electionService, IValidator<GetElectionUsersQuery> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<List<UserEntity>?> Handle(GetElectionUsersQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var electionUsers = await _electionService.GetElectionUsersAsync(request.ElectionId);

        return electionUsers;
    }
}