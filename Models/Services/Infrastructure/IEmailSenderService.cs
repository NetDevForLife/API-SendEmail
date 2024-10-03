namespace Models.Services.Infrastructure;

public interface IEmailSenderService
{
	Task SendEmailAsync(InputMailSender model);
}