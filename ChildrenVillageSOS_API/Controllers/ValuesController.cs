using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChildrenVillageSOS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("get-string")]
        public IActionResult GetString()
        {
            string result = "Hello, this is a string!";
            return Ok(result);
        }

    }
    
}
