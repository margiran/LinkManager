using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkManagerApi.Contracts.V1;
using LinkManagerApi.Contracts.V1.Requests;
using LinkManagerApi.Contracts.V1.Responses;
using LinkManagerApi.Domain;
using LinkManagerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.Controllers.V1
{
    [ApiController]
    public class LinkController :ControllerBase
    {
        private readonly ILinkService _linkService;
        public LinkController(ILinkService linkService)
        {
           _linkService=linkService;
        }

        [Route(ApiRoutes.Links.GetAll)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _linkService.GetAll());
        }

        [Route(ApiRoutes.Links.Get, Name ="Get")]
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var link=await _linkService.GetById(id);
            if (link == null ) return NotFound();

            var response= new LinkResponse {
                Id = link.Id,
                Name= link.Name
            };
            return Ok(response);
        }


        [Route(ApiRoutes.Links.Create)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLinkRequest request )
        {
            var link= new Link{ Name = request.Name};
           await  _linkService.Create(link);

            var response= new LinkResponse { Id= link.Id, Name=link.Name};
            return CreatedAtRoute(nameof(Get),new{id=response.Id}, response);
        }

        [Route(ApiRoutes.Links.Update)]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLinkRequest updateLinkRequest)
        {
            var link= new Link{
                Id= id,
                Name= updateLinkRequest.Name
            };

            var update=await _linkService.Update(link);
            if(update)
                return Ok(link);

            return NotFound();

        }

        [Route(ApiRoutes.Links.Delete)]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var delete= await _linkService.Delete(id);
            if (delete)
                return NoContent();
            
            return NotFound();

        }

    }
}