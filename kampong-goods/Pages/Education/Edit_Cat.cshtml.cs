using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Education
{
	public class Edit_CatModel : PageModel
    {
        private readonly FAQCatService _faqCatService;

        public Edit_CatModel(FAQCatService faqCatService)
        {
            _faqCatService = faqCatService;
        }

        [BindProperty]
        public FAQCategory myFAQCat { get; set; } = new();

        public IActionResult OnGet(string ID)
        {
            FAQCategory? FAQCAT = _faqCatService.GetFAQCatById(ID);
            if (FAQCAT != null)
            {
                myFAQCat = FAQCAT;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQCAT( ID {0}) not found", ID);
                return Redirect("/Education/FAQCat_List");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _faqCatService.UpdateFAQCat(myFAQCat);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("FAQ {0} is Edited", myFAQCat.FAQCatId);
                return Redirect("/Education/FAQCat_List");
            }
            return Page();
        }
        
    }
}
