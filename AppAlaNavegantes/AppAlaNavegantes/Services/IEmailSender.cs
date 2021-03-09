using System.Threading.Tasks;

namespace AppAlaNavegantes.Services
{
    public interface IEmailSender
    {
        Task EmailSenderAsync(string name, string subject, string message);
    }
}
