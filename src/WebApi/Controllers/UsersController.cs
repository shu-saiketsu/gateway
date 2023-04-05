using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Users.Commands.BlockUser;
using Saiketsu.Gateway.Application.Users.Commands.CreateUser;
using Saiketsu.Gateway.Application.Users.Commands.DeleteUser;
using Saiketsu.Gateway.Application.Users.Commands.UnblockUser;
using Saiketsu.Gateway.Application.Users.Queries.GetUser;
using Saiketsu.Gateway.Application.Users.Queries.GetUsers;
using Saiketsu.Gateway.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Gateway.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all users")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved users successfully", typeof(List<UserEntity>))]
    [Authorize("read:users")]
    public async Task<IActionResult> GetAll()
    {
        var message = new GetUsersQuery();
        var response = await _mediator.Send(message);

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created user successfully", typeof(UserEntity))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create user")]
    [Authorize("create:users")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == null)
            return BadRequest();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieve a user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved user successfully", typeof(UserEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User does not exist")]
    [Authorize("read:users")]
    public async Task<IActionResult> Get(string id)
    {
        var message = new GetUserQuery { Id = id };
        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpPost("{id}/block")]
    [SwaggerOperation(Summary = "Block a user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Blocked user successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "User does not exist")]
    [Authorize("sanction:users")]
    public async Task<IActionResult> Block(string id)
    {
        var message = new BlockUserCommand { Id = id };
        var response = await _mediator.Send(message);

        if (!response)
            return BadRequest();

        return Ok();
    }

    [HttpPost("{id}/unblock")]
    [SwaggerOperation(Summary = "Unblocks a user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Unblocked user successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "User does not exist")]
    [Authorize("sanction:users")]
    public async Task<IActionResult> Unblock(string id)
    {
        var message = new UnblockUserCommand { Id = id };
        var response = await _mediator.Send(message);

        if (!response)
            return BadRequest();

        return Ok();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete a user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted user successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User does not exist")]
    [Authorize("delete:users")]
    public async Task<IActionResult> Delete(string id)
    {
        var request = new DeleteUserCommand { Id = id };

        var response = await _mediator.Send(request);

        if (response)
            return Ok();

        return NotFound();
    }
}