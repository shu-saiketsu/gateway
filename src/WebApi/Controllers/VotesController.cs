using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;
using Saiketsu.Gateway.Application.Votes.Queries.CalculateVote;
using Saiketsu.Gateway.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Gateway.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class VotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VotesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cast a new vote")]
    [SwaggerResponse(StatusCodes.Status200OK, "Cast vote successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to cast vote")]
    [Authorize]
    public async Task<IActionResult> CastVote(CreateVoteCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpGet("{electionId:int}")]
    [SwaggerOperation(Summary = "Calculate votes for an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Calculated votes successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to calculate votes")]
    [Authorize]
    public async Task<IActionResult> CalculateVote(int electionId)
    {
        var query = new CalculateVoteQuery { ElectionId = electionId };

        var response = await _mediator.Send(query);

        if (response == null) return BadRequest();

        return Ok(response);
    }
}