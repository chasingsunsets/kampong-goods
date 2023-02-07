using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Education
{
    public class AddFAQCategoryModel : PageModel
    {
        private readonly CustomerService _customerService;
        private UserManager<AppUser> userManager { get; }
        private readonly FAQCatService _faqCatService;
        [BindProperty]
        public FAQCategory myFAQCat { get; set; }
        public List<FAQCategory> FAQCatlist { get; set; }
        public string staffName { get; set; }


        public void OnGet()
        {
            FAQCatlist = _faqCatService.GetAll();
        }

        public AddFAQCategoryModel(CustomerService customerService, UserManager<AppUser> userManager, FAQCatService faqCatService)
        {
            _customerService = customerService;
            this.userManager = userManager;
            _faqCatService = faqCatService;
        }


        public IActionResult OnPost()
        {


            if (ModelState.IsValid)
            {


                FAQCategory? faqcat = _faqCatService.GetFAQCatById(myFAQCat.Id);
                if (faqcat != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format(
                    "FAQID {0} alreay exists", myFAQCat.CategoryName);
                    return Page();
                }

                _faqCatService.AddFAQCAt(myFAQCat);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is added", myFAQCat.Id);
                return Redirect("/Education");
            }
            return Page();
        }
    }
}
