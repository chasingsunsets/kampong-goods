using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Reflection.Metadata;
using kampong_goods.Pages.Vouchers;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace kampong_goods.Pages.Education
{
    public class FAQDetailModel : PageModel
    {

        private UserManager<AppUser> userManager { get; }
        private readonly FAQService _FAQService;
        private readonly UserManager<AppUser> _userManager;

        public FAQDetailModel(FAQService faqService, UserManager<AppUser> userManager)
        {
            _FAQService = faqService;
            _userManager = userManager;
        }




        public AppUser user { get; set; } = new();
        public FAQ myFAQ { get; set; } = new();
        public static List<FAQCategory> FAQCatlist { get; set; } = new();

        public IActionResult OnGet(string ID)
        {
            FAQ? FAQ = _FAQService.GetFAQById(ID);

            if (FAQ != null)
            {
                myFAQ = FAQ;
                myFAQ.ClickTime += 1;
                _FAQService.UpdateFAQ(myFAQ);
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQ({0}) not found", ID);
                return Redirect("/Educaion/List_Cust");
            }
        }


        public void OnGetClickTimeCount(string ID)
        {
            FAQ FAQ = _FAQService.GetFAQById(ID);
            myFAQ = FAQ;
            myFAQ.ClickTime += 1;
            _FAQService.UpdateFAQ(myFAQ);
        }

        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _FAQService.UpdateFAQ(myFAQ);
        //        TempData["FlashMessage.Type"] = "success";
        //        TempData["FlashMessage.Text"] = string.Format(
        //        "FAQ{0} is updated", myFAQ.FAQId);
        //    }
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync(string ID)
        {
            //get FAQ
            FAQ? FAQ = _FAQService.GetFAQById(ID);
            myFAQ = FAQ;


            //gets the logged in user
            var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;
            user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            string? emailAddress = user.Email;
            string? title = "Report from User: " + user.Id;
            string? content = "you receive a report on FAQ id: " + myFAQ.FAQId;


            MailMessage message = new MailMessage();
            message.To.Add("q2467231710@gmail.com");
            message.Subject = title;
            message.Body = content;
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
            return Redirect("/Education/List_Cust");
        }

    }
}