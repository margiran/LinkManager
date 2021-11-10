using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinkManagerApi.Commands;
using LinkManagerApi.Contracts.V1;
using LinkManagerApi.Contracts.V1.Requests;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Domain;
using LinkManagerApi.Queries;
using LinkManagerApi.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.Controllers.V1
{
    [ApiController]
    public class LinkController :ControllerBase
    {
        private readonly IMediator _mediator;

        public LinkController( IMediator mediator )
        {
           _mediator=mediator;
        }

        [Route(ApiRoutes.Links.GetAll)]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string updateAt )
        {
            var query= new GetAllLinksQuery(updateAt);
            var result= await _mediator.Send(query);
            Console.WriteLine(DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
            return Ok(result);
        }

        [Route(ApiRoutes.Links.Get, Name ="Get")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var query=new  GetLinkByIdQuery(id);
            var result= await _mediator.Send(query);

            if (result == null ) 
                return NotFound();
            return Ok(result);
        }

        // [Route(ApiRoutes.Links.GetByUpdateAfter)]
        // [HttpGet]
        // public async Task<IActionResult> GetUpdatedAfter([FromQuery] DateTimeOffset updateAt)
        // {
        //     var UpdateAt = DateTimeOffset.UtcNow;
        //     if (!DateTimeOffset.TryParse(updateAt.ToString(),out UpdateAt))
        //         return BadRequest(updateAt);
        //     var query= new GetLinksUpdatedAfterQuery(UpdateAt);
        //     var result= await _mediator.Send(query);
        //     return Ok(result);
        // }

        [Route(ApiRoutes.Links.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkCommand command )
        {
            if (command== null || !ModelState.IsValid )
                return BadRequest(ModelState);
            var response= await _mediator.Send(command);
            return CreatedAtRoute(nameof(Get),new{id=response.Id}, response);
        }

        [Route(ApiRoutes.Links.Update)]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLinkCommand command)
        {
            if (command==null || !ModelState.IsValid )
                return BadRequest(ModelState);
            command.Id=id;
            var result= await _mediator.Send(command);

            if (result == null )
                return NotFound();

            return Ok(result);
        }

        [Route(ApiRoutes.Links.Delete)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command= new DeleteLinkCommand(id);
            var result = await _mediator.Send(command);

            if (result)
                return NoContent();

            return NotFound();
        }

    }
}