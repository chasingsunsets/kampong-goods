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

        public EditModel(FAQService faqService)
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
                return Redirect("/Materials");
            }
        }

        public IActionResult OnPost()
        {


            if (ModelState.IsValid)
            {



                _faqService.UpdateFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is Edited", myFAQ.Question);
                return Redirect("/Education");
            }
            return Page();
        }
    }
}


namespace EDP_AssN.Pages.Materials
{
    public class DetailsModel : PageModel
    {

        

    }
}