using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Diagnostics;


namespace kampong_goods.Pages.Education
{

    public class AddFAQModel : PageModel
    {
        private readonly CustomerService _customerService;
        private UserManager<AppUser> userManager { get; }

        private readonly StaffService _staffService;

        private readonly FAQService _faqService;


        public string staffName { get; set; }

        public void OnGet()
        {

        }

        public AddFAQModel(FAQService faqService, CustomerService customerService, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            this.userManager = userManager;
            _faqService = faqService;
                    }



        [BindProperty]
        public FAQ myFAQ { get; set; }



        public IActionResult OnPost()
        {
           

            if (ModelState.IsValid)
            {


                FAQ? faq = _faqService.GetFAQById(myFAQ.FAQId);
                if (faq != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format(
                    "FAQID {0} alreay exists", myFAQ.Question);
                    return Page();
                }

                _faqService.AddFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is added", myFAQ.Question);
                return Redirect("/Education");
            }
            return Page();
        }
    }
}