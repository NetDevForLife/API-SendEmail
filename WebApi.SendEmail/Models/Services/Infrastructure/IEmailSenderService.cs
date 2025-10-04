using Models.InputModels;

namespace Models.Services.Infrastructure;

public interface IEmailSenderService
{
	Task SendEmailsAttachmentAsync(InputMailSender model, IFormFile allegato);
	Task SendEmailsAttachmentsAsync(InputMailSender model, IFormFileCollection allegati);
}