using System.Threading.Tasks;
using K94Warriors.ViewModels.EmailNotifications;

namespace K94Warriors.Email
{
    public interface IMailer
    {
        Task Send(string from, string to, string subject, string body);
        Task Send(EmailViewModel viewModel);
    }
}