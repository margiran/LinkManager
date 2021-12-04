using System.Threading;
using System;
using System.Threading.Tasks;
using LinkManagerApi.Commands;
using LinkManagerApi.Queries;
using LinkManagerApi.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class LinkController : ControllerBase
{
    private readonly IMediator _mediator;

    public LinkController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route(ApiRoutes.Links.GetAll)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery(Name = "updateAfter")] string updateAfter,CancellationToken cancellationToken)
    {
        var query = new GetAllLinksQuery(updateAfter);
        var result = await _mediator.Send(query,cancellationToken);
        return Ok(result);
    }

    [Route(ApiRoutes.Links.Get, Name = "Get")]
    [HttpGet]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetLinkByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [Route(ApiRoutes.Links.Create)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLinkCommand command)
    {
        Console.WriteLine("create link");
        if (command == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        var response = await _mediator.Send(command);
        return CreatedAtRoute(nameof(Get), new { id = response.Id }, response);
    }

    [Route(ApiRoutes.Links.Update)]
    [HttpPut]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLinkCommand command)
    {
        if (command == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        command.Id = id;
        var result = await _mediator.Send(command);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [Route(ApiRoutes.Links.Delete)]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteLinkCommand(id);
        var result = await _mediator.Send(command);

        if (result)
            return NoContent();

        return NotFound();
    }

}
