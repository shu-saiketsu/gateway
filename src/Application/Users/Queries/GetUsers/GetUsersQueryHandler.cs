using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserEntity>>
{
    private readonly IUserService _userService;
    private readonly IValidator<GetUsersQuery> _validator;

    public GetUsersQueryHandler(IValidator<GetUsersQuery> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    public async Task<List<UserEntity>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var users = await _userService.GetUsersAsync();

        return users;
    }
}