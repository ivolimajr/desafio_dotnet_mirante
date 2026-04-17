using Microsoft.AspNetCore.Mvc;

namespace Api.Reports.Controllers
{
    /// <summary>
    /// Endpoint de status
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HeathController : ControllerBase
    {
        /// <summary>
        /// Saude da API, retorna status 200 e data atual.
        /// </summary>
        /// <returns></returns>
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
