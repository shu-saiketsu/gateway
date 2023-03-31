using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElections;
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
    public async Task<IActionResult> Create([FromBody] CreateElectionCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == null)
            return BadRequest();

        return Ok(response);
    }

    [HttpPost("add-user")]
    [SwaggerOperation(Summary = "Adds a user to an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created election user link successfully")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create link")]
    public async Task<IActionResult> AddUserToElection([FromBody] AddUserToElectionCommand command)
    {
        var response = await _mediator.Send(command);

        if (response == false) return BadRequest();

        return Ok();
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Retrieve an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved election successfully", typeof(ElectionEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    public async Task<IActionResult> Get(int id)
    {
        var request = new GetElectionQuery { Id = id };

        var response = await _mediator.Send(request);

        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete an election")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted election successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Election does not exist")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteElectionCommand { Id = id };

        var response = await _mediator.Send(request);

        if (response == false)
            return NotFound();

        return Ok();
    }
}