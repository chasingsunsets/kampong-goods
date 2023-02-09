using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Education
{
    public class DetailModel : PageModel
    {
        private readonly FAQService _faqService;

        public DetailModel(FAQService faqService)
        {
            _faqService = faqService;
        }

        [BindProperty]
        public FAQ myFAQ { get; set; } = new();


        public IActionResult OnGet(string ID)
        {
            FAQ? FAQ = _faqService.GetFAQById(ID);
            if (FAQ != null)
            {
                myFAQ = FAQ;
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
    }
}
