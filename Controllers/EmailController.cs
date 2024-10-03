namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController(IEmailSenderService emailService) : ControllerBase
{
	private readonly IEmailSenderService emailService = emailService;

	[HttpGet("Welcome")]
	public IActionResult Welcome()
	{
		return Ok(string.Concat("Ciao sono le ore: ", DateTime.Now.ToLongTimeString()));
	}

	[HttpPost("InvioEmail")]
	public async Task<IActionResult> InvioEmail([FromForm] InputMailSender model)
	{
		try
		{
			await emailService.SendEmailAsync(model);
			return Ok();
		}
		catch
		{
			throw new Exception();
		}
	}
}