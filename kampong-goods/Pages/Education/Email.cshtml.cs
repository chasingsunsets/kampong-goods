using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Diagnostics;

namespace kampong_goods.Pages.Education
{
	public class EmailModel : PageModel
    {
        private readonly FAQService _FAQService;
        public List<FAQ> FAQList { get; set; } = new();

        public EmailModel(FAQService faqService)
        {
            _FAQService = faqService;
        }

        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            MailMessage message = new MailMessage();
            message.To.Add("q2467231710@gmail.com");
            message.Subject = "Test Email";
            message.Body = "you receive a test email!!!";
            message.IsBodyHtml = false;
            message.From = new MailAddress("q2467231710@gmail.com");

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;



            client.Credentials = new NetworkCredential("q2467231710@gmail.com", "nqwiqatsrtbvybjr");
            await client.SendMailAsync(message);
            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = string.Format("Message send");
            return Redirect("/Education/Index");
        }
    }
}
