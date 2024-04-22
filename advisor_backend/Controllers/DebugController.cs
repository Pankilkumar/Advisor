using Microsoft.AspNetCore.Mvc;

namespace advisor_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        
        [HttpGet("test")]
        public IActionResult Test()
        {
          
            var message = "Debug test message";
            var result = message.ToUpper();
            return Ok(result);
        }
    }
}