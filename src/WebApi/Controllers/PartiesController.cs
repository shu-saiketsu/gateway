using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Parties.Commands.CreateParty;
using Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;
using Saiketsu.Gateway.Application.Parties.Queries.GetParties;
using Saiketsu.Gateway.Application.Parties.Queries.GetParty;
using Saiketsu.Gateway.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Gateway.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class PartiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PartiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all parties")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved parties successfully", typeof(List<PartyEntity>))]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetPartiesQuery();
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new party")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created party successfully", typeof(PartyEntity))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create party")]
    public async Task<IActionResult> Add([FromBody] [Required] CreatePartyCommand command)
    {
        var candidate = await _mediator.Send(command);

        if (candidate == null)
            return BadRequest();

        return Ok(candidate);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Retrieve a party")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved party successfully", typeof(PartyEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Party does not exist")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var message = new GetPartyQuery { Id = id };

        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a party")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted party successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Party does not exist")]
    public async Task<IActionResult> Delete([Required] int id)
    {
        var message = new DeletePartyCommand { Id = id };
        var success = await _mediator.Send(message);

        if (success) return Ok();

        return NotFound();
    }
}