using Models.InputModels;
using Models.Options;

namespace Models.Services.Infrastructure;

public class MailKitEmailSender(IOptionsMonitor<SmtpOptions> smtpOptionsMonitor) : IEmailSenderService
{
	private readonly IOptionsMonitor<SmtpOptions> smtpOptionsMonitor = smtpOptionsMonitor;

	public async Task SendEmailsAttachmentAsync(InputMailSender model, IFormFile allegato)
	{
		try
		{
			var options = this.smtpOptionsMonitor.CurrentValue;

			using SmtpClient client = new();

			await client.ConnectAsync(options.Host, options.Port, options.Security);

			if (!string.IsNullOrEmpty(options.Username))
			{
				await client.AuthenticateAsync(options.Username, options.Password);
			}

			MimeMessage message = new();

			message.From.Add(MailboxAddress.Parse(options.Sender));
			message.To.Add(MailboxAddress.Parse(model.RecipientEmail));
			message.Subject = model.Subject;

			var builder = new BodyBuilder();

			if (allegato != null)
			{
				byte[] fileBytes;

				if (allegato.Length > 0)
				{
					using (var ms = new MemoryStream())
					{
						allegato.CopyTo(ms);
						fileBytes = ms.ToArray();
					}

					builder.Attachments.Add(allegato.FileName, allegato.OpenReadStream(), ContentType.Parse(allegato.ContentType));
				}
			}

			builder.HtmlBody = model.HtmlMessage;
			message.Body = builder.ToMessageBody();

			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}
		catch
		{
			throw new Exception();
		}
	}

	public async Task SendEmailsAttachmentsAsync(InputMailSender model, IFormFileCollection allegati)
	{
		try
		{
			var options = this.smtpOptionsMonitor.CurrentValue;

			using SmtpClient client = new();

			await client.ConnectAsync(options.Host, options.Port, options.Security);

			if (!string.IsNullOrEmpty(options.Username))
			{
				await client.AuthenticateAsync(options.Username, options.Password);
			}

			MimeMessage message = new();

			message.From.Add(MailboxAddress.Parse(options.Sender));
			message.To.Add(MailboxAddress.Parse(model.RecipientEmail));
			message.Subject = model.Subject;

			var builder = new BodyBuilder();

			if (allegati != null)
			{
				byte[] fileBytes;

				foreach (var file in allegati)
				{
					if (file.Length > 0)
					{
						using (var ms = new MemoryStream())
						{
							file.CopyTo(ms);
							fileBytes = ms.ToArray();
						}

						builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
					}
				}
			}

			builder.HtmlBody = model.HtmlMessage;
			message.Body = builder.ToMessageBody();

			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}
		catch
		{
			throw new Exception();
		}
	}
}