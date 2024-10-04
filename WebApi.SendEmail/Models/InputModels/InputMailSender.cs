namespace API_SendEmail.Models.InputModels;

public class InputMailSender
{
	[Required(ErrorMessage = "L'indirizzo email è obbligatorio"), EmailAddress]
	public string RecipientEmail { get; set; }

	[Required(ErrorMessage = "L'oggetto è obbligatorio")]
	public string Subject { get; set; }

	[Required(ErrorMessage = "Il messaggio è obbligatorio")]
	public string HtmlMessage { get; set; }
}