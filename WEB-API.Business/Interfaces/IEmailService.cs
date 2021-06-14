using System.Threading.Tasks;

namespace WEB_API.Business.Interfaces
{
    public interface IEmailService
    {
        Task Send(string email ,string subject, string confirmationUrl);
    }
}