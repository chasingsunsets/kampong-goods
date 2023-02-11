using SendGrid;
using SendGrid.Helpers.Mail;

namespace kampong_goods.Services
{
	public interface IMailService
	{

		Task SendEmailAsync(string toEmail, string subject, string content);
	}

	public class SendGridMailService : IMailService
	{
		private IConfiguration _configuration;

		public SendGridMailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

        public async Task SendEmailAsync(string toEmail, string subject, string content)
		{
			var apikey = _configuration["SendGridAPIKey"];
			var client = new SendGridClient(apikey);
			var from = new EmailAddress("limmeiling108@gmail.com", "Kampong Goods");
			var to = new EmailAddress(toEmail);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
			var response = await client.SendEmailAsync(msg);
		}
    }
}
