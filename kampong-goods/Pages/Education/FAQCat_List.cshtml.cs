using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Xml.Linq;

namespace kampong_goods.Pages.Education
{
    public class FAQCat_ListModel : PageModel
    {
        private readonly FAQCatService _FAQService;
        public FAQCategory myFAQ { get; set; } = new();
        public string searchItem { get; set; }


        public List<FAQCategory> FAQlist { get; set; } = new();

        public List<FAQCategory> FAQlist_sorted { get; set; } = new();
        public FAQCat_ListModel(FAQCatService faqCatService)
        {
            _FAQService = faqCatService;
        }

        //FAQ retreieve
        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();

        }

        //Delete
        public void OnGetDeleteFAQ(string ID)
        {
            _FAQService.DeleteFAQCat(ID);
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format(
            "FAQ{0} is Delete", ID);
            FAQlist = _FAQService.GetAll();
        }


        



    }
}
