using System.Threading.Tasks;
using API_SendEmail.Models.InputModels;

namespace API_SendEmail.Models.Services.Infrastructure;

public interface IEmailSenderService
{
    Task SendEmailAsync(InputMailSender model);
}