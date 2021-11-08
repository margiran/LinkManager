using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinkManagerApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.Controllers.V1
{
    [ApiController]
    public class LinkController :ControllerBase
    {
        List<link> _links;
        public LinkController()
        {
            _links=new List<link>();
            for (var i = 0; i < 4 ; i++)
            {
                _links.Add(new link{Id=Guid.NewGuid().ToString() });
            }

            
        }
        [HttpGet("api/v1")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_links);
        }
    }
}