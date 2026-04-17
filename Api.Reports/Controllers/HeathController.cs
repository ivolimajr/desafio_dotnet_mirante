using Microsoft.AspNetCore.Mvc;

namespace Api.Reports.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeathController : ControllerBase
    {
        [HttpGet(Name = "Health")]
        [ProducesResponseType(200)]
        public object Get()
        {
            return new {
                status = StatusCodes.Status200OK,
                date = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss")
            };
            
        }
    }
}
