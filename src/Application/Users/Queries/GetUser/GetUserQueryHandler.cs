﻿using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Queries.GetUser;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserEntity?>
{
    private readonly IUserService _userService;
    private readonly IValidator<GetUserQuery> _validator;

    public GetUserQueryHandler(IValidator<GetUserQuery> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    public async Task<UserEntity?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var users = await _userService.GetUserAsync(request.Id);

        return users;
    }
}