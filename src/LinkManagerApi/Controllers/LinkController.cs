using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LinkManagerApi.Controllers
{
    [ApiController]
    public class LinkController :ControllerBase
    {
        [HttpGet("api/v1")]
        public async Task<IActionResult> GetAll()
        {
            return null;
        }
    }
}