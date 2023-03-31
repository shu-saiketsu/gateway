using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;
using Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;
using Saiketsu.Gateway.Application.Candidates.Queries.GetCandidates;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Saiketsu.Gateway.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CandidatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CandidatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieve all candidates")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved candidates successfully", typeof(List<CandidateEntity>))]
    public async Task<IActionResult> GetAll()
    {
        var message = new GetCandidatesQuery();

        var response = await _mediator.Send(message);

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new candidate")]
    [SwaggerResponse(StatusCodes.Status200OK, "Created candidate successfully", typeof(CandidateEntity))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Unable to create candidate")]
    public async Task<IActionResult> Add([FromBody] [Required] CreateCandidateCommand command)
    {
        var candidate = await _mediator.Send(command);

        if (candidate == null) return BadRequest();

        return Ok(candidate);
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Retrieve a candidate")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retrieved candidate successfully", typeof(CandidateEntity))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Candidate does not exist")]
    public async Task<IActionResult> Get([Required] int id)
    {
        var message = new GetCandidateQuery { Id = id };

        var response = await _mediator.Send(message);

        if (response == null)
            return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a candidate")]
    [SwaggerResponse(StatusCodes.Status200OK, "Deleted candidate successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Candidate does not exist")]
    public async Task<IActionResult> Delete([Required] int id)
    {
        var message = new DeleteCandidateCommand { Id = id };
        var success = await _mediator.Send(message);

        if (success) return Ok();

        return NotFound();
    }
}