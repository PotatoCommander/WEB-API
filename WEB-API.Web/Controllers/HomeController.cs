using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("GetInfo")]
        public IActionResult GetInfo()
        {
            return Ok("Hello world!");
        }
    }
}