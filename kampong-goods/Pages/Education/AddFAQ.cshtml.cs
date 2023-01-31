using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace kampong_goods.Pages.Education
{
    public class AddFAQModel : PageModel
    {
        private readonly FAQService _faqService;

        public AddFAQModel(FAQService faqService)
        {
            _faqService = faqService;
                    }



        [BindProperty]
        public FAQ myFAQ { get; set; }


                    public void OnGet()
                    {
        }

        public IActionResult OnPost()
        {
           

            if (ModelState.IsValid)
            {


                FAQ? faq = _faqService.GetFAQById(myFAQ.FAQId);
                if (faq != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format(
                    "FAQID {0} alreay exists", myFAQ.FAQId);
                    return Page();
                }

                _faqService.AddFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is added", myFAQ.FAQId);
                return Redirect("/Materials");
            }
            return Page();
        }
    }
}