using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Reflection.Metadata;
using kampong_goods.Pages.Vouchers;

namespace kampong_goods.Pages.Education
{
    public class FAQDetailModel : PageModel
    {

        private readonly FAQService _FAQService;

        public FAQDetailModel(FAQService faqService)
        {
            _FAQService = faqService;
        }

        public FAQ myFAQ { get; set; }
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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _FAQService.UpdateFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQ{0} is updated", myFAQ.FAQId);
            }
            return Page();
        }


    }
}