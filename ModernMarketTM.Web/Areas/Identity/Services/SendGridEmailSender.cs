using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ModernMarketTM.Web.Areas.Identity.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        public IConfiguration Configuration { get; set; }

        public SendGridEmailSender(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.Configuration.GetSection("SendGrid:ApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("vesopk979@mail.bg", "Private Modern Market TM Admin");
            var to = new EmailAddress(email,email);
            var message = MailHelper.CreateSingleEmail(from, to, email, htmlMessage, htmlMessage);
            await client.SendEmailAsync(message);
        }
    }
}
