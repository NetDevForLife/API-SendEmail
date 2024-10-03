namespace Models.InputModels;

public class InputMailSender
{
	[Required(ErrorMessage = "L'indirizzo email è obbligatorio"), EmailAddress, Display(Name = "Destinatario")]
	public string RecipientEmail { get; set; }

	[Required(ErrorMessage = "L'oggetto è obbligatorio"), Display(Name = "Oggetto")]
	public string Subject { get; set; }

	[Required(ErrorMessage = "Il messaggio è obbligatorio"), Display(Name = "Messaggio")]
	public string HtmlMessage { get; set; }

	[Display(Name = "Allegato/i")]
	public List<IFormFile> Attachments { get; set; }
}