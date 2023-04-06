using FluentValidation;

namespace Saiketsu.Gateway.Application.Users.Queries.GetUser;

public sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}