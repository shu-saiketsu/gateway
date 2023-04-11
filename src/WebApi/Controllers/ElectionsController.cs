using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;
using Saiketsu.Gateway.Application.Elections.Queries.GetElections;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;
using Saiketsu.Gateway.Domain.Entities;
using Saiketsu.Gateway.Domain.Entities.Election;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Gateway.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ElectionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ElectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all elections")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved elections successfully", typeof(List<ElectionEntity>))]
    [Authorize("read:elections")]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetElectionsQuery();

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created election successfully", typeof(ElectionEntity))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unable to create election")]
    [Authorize("create:elections")]
    public async Task<IActionResult> Create([FromBody] CreateElectionCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == null)
            return BadRequest();

        return Ok(response);
    }

    [HttpGet("{electionId:int}")]
    [SwaggerOperation(Summary = "Retrieve an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved election successfully", typeof(ElectionEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    [Authorize]
    public async Task<IActionResult> Get(int electionId)
    {
        var request = new GetElectionQuery { Id = electionId };

        var response = await _mediator.Send(request);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet("{electionId:int}/users")]
    [SwaggerOperation(Summary = "Retrieve a election users")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved election users successfully", typeof(List<UserEntity>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    [Authorize("read:elections")]
    public async Task<IActionResult> GetUsers(int electionId)
    {
        var request = new GetElectionUsersQuery { ElectionId = electionId };

        var response = await _mediator.Send(request);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpGet("users/{userId}")]
    [SwaggerOperation(Summary = "Retrieve elections that users can vote in",
        Description =
            "The developer can set the eligible query to false to view every election the user has voted in or can vote in. If set to true, it will only return what the user can currently vote in.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved elections successfully", typeof(List<ElectionEntity>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User does not exist")]
    public async Task<IActionResult> GetUserElections(string userId, [FromQuery] [Required] bool eligible)
    {
        var request = new GetElectionsForUserQuery { UserId = userId, Eligible = eligible };

        var response = await _mediator.Send(request);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpPost("{electionId:int}/users/{userId}")]
    [SwaggerOperation(Summary = "Adds a user to an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created election user link successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create link")]
    [Authorize("update:elections")]
    public async Task<IActionResult> AddUserToElection(int electionId, string userId)
    {
        var command = new AddUserToElectionCommand { ElectionId = electionId, UserId = userId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpDelete("{electionId:int}/users/{userId}")]
    [SwaggerOperation(Summary = "Removes a user from an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Removed election user link successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to remove link")]
    [Authorize("update:elections")]
    public async Task<IActionResult> RemoveUserFromElection(int electionId, string userId)
    {
        var command = new RemoveUserFromElectionCommand { ElectionId = electionId, UserId = userId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpGet("{electionId:int}/candidates")]
    [SwaggerOperation(Summary = "Retrieve a election candidates")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved election candidates successfully",
        typeof(List<CandidateEntity>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    [Authorize]
    public async Task<IActionResult> GetCandidates(int electionId)
    {
        var request = new GetElectionCandidatesQuery { ElectionId = electionId };

        var response = await _mediator.Send(request);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpPost("{electionId:int}/candidates/{candidateId:int}")]
    [SwaggerOperation(Summary = "Adds a candidate to an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created election candidate link successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create link")]
    [Authorize("update:elections")]
    public async Task<IActionResult> AddCandidateToElection(int electionId, int candidateId)
    {
        var command = new AddCandidateToElectionCommand { ElectionId = electionId, CandidateId = candidateId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpDelete("{electionId:int}/candidates/{candidateId:int}")]
    [SwaggerOperation(Summary = "Removes a candidate from an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Removed election candidate link successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to remove link")]
    [Authorize("update:elections")]
    public async Task<IActionResult> RemoveCandidateFromElection(int electionId, int candidateId)
    {
        var command = new RemoveCandidateFromElectionCommand { ElectionId = electionId, CandidateId = candidateId };

        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpDelete("{electionId:int}")]
    [SwaggerOperation(Summary = "Delete an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted election successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    [Authorize("delete:elections")]
    public async Task<IActionResult> Delete(int electionId)
    {
        var request = new DeleteElectionCommand { Id = electionId };

        var response = await _mediator.Send(request);

        if (response == false)
            return NotFound();

        return Ok();
    }
}