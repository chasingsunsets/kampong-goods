using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace kampong_goods.Pages.Education
{
    public class EditModel : PageModel
    {
        private readonly FAQService _faqService;
        private readonly FAQCatService _faqCatService;

        public EditModel(FAQService faqService, FAQCatService faqcatService)
        {
            _faqService = faqService;
            _faqCatService = faqcatService;
        }

        [BindProperty]
        public FAQ myFAQ { get; set; } = new();


        public static List<FAQCategory> FAQCatlist { get; set; } = new();


        public IActionResult OnGet(string ID)
        {
            FAQCatlist = _faqCatService.GetAll();
            FAQ? FAQ = _faqService.GetFAQById(ID);
            if (FAQ != null)
            {
                myFAQ = FAQ;
                myFAQ.FAQCategory = _faqCatService.GetFAQCatById(FAQ.FAQCatId);
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQ(ID {0}) not found", ID);
                return Redirect("/Education");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                myFAQ.Publish = false;
                _faqService.UpdateFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is Edited", myFAQ.Question);
                return Redirect("/Education");
            }
            return Page();
        }
    }
}