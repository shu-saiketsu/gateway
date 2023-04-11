using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQueryValidator : AbstractValidator<GetElectionUsersQuery>
{
    public GetElectionUsersQueryValidator()
    {
        RuleFor(x => x.ElectionId)
            .NotEmpty();
    }
}