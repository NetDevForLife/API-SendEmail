using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_SendEmail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            return Ok(string.Concat("Ciao sono le ore: ", DateTime.Now.ToLongTimeString()));
        }
    }
}