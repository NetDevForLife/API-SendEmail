namespace API_SendEmail.Endpoints;

public class EmailEndpoints : IEndpointRouteHandler
{
	public static void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var emailApiGroup = endpoints
			.MapGroup("/api/invioemail")
			.WithOpenApi();

		emailApiGroup.MapGet("/welcome", GetWelcomeMessage)
			.WithDescription("Endpoint for welcome message");

		emailApiGroup.MapPost("/sendemailwithattachment", SendEmailsAttachment)
			.DisableAntiforgery()
			.WithDescription("Endpoint for sending email with attachment");

		emailApiGroup.MapPost("/sendemailwithattachments", SendEmailsAttachments)
			.DisableAntiforgery()
			.WithDescription("Endpoint for sending email with attachments");
	}

	public static string GetWelcomeMessage()
	{
		return $"Ciao sono le ore: {DateTime.Now.ToLongTimeString()}";
	}

	public static async Task<Results<Ok, BadRequest>> SendEmailsAttachment(IFormFile allegato,
		[AsParameters] InputMailSender request, IEmailSenderService emailService)
	{
		try
		{
			await emailService.SendEmailsAttachmentAsync(request, allegato);
			return TypedResults.Ok();
		}
		catch
		{
			return TypedResults.BadRequest();
		}
	}

	public static async Task<Results<Ok, BadRequest>> SendEmailsAttachments(IFormFileCollection allegati,
			[AsParameters] InputMailSender request, IEmailSenderService emailService)
	{
		try
		{
			await emailService.SendEmailsAttachmentsAsync(request, allegati);
			return TypedResults.Ok();
		}
		catch
		{
			return TypedResults.BadRequest();
		}
	}
}