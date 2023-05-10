using Microsoft.AspNetCore.Mvc;

namespace SoundSoulAPI.Controllers.Creator
{
    [ApiController]
    [Route("api/creator")]
    public class CreatorController : BaseController
    {
        protected CreatorController(IConfiguration configuration) : base(configuration)
        {
        }
        [HttpGet("index")]
        public async Task<IActionResult> Index([FromQuery])
        {
            return GetResponse(await new );
        }
    }
}
