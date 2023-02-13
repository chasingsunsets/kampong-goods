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

        private readonly FAQCatService _faqCatService;


        public FAQCategory myFAQCat { get; set; }
        public static List<FAQCategory> FAQCatlist { get; set; } = new();
        public string staffName { get; set; }

        public void OnGet()
        {
            FAQCatlist = _faqCatService.GetAll();
        }

        public AddFAQModel(FAQService faqService, CustomerService customerService, UserManager<AppUser> userManager, FAQCatService faqCatService)
        {
            _customerService = customerService;
            this.userManager = userManager;
            _faqService = faqService;
            _faqCatService = faqCatService;
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

                myFAQ.ClickTime = 0;
                myFAQ.Publish = false;
                myFAQ.ReportCount = 0;
/*                myFAQ.FAQCatId = _faqCatService.GetIdByFAQCat();*/
                _faqService.AddFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is added", myFAQ.Question);
                return Redirect("/Education");
            }
            return Page();
        }
    }
}