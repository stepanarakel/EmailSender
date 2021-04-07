using System.Threading.Tasks;
namespace Postman.Interfaces
{
    public interface IEmailService
    {
        string SendEmail(Message message);
        Task<string> SendEmailAsync(Message message);
    }
}
