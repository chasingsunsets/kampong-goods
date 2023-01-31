using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using System.Xml.Linq;


namespace kampong_goods.Pages.Education
{
    public class IndexModel : PageModel
    {
        private readonly FAQService _FAQService;
        public FAQ myFAQ { get; set; } = new();

        public List<FAQ> FAQlist { get; set; } = new();
        public IndexModel(FAQService faqService)
        {
            _FAQService = faqService;
        }

        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();

        }

        public void OnGetDeleteFAQ(string ID)
        {
            _FAQService.DeleteFAQ(ID);
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = string.Format(
            "FAQ{0} is Delete", ID);
            FAQlist = _FAQService.GetAll();
        }







        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                /*_FAQService.DeleteFAQ(myFAQ);
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format(
                "FAQ{0} is Delete", myFAQ.ID);*/
            }
            return Page();

            //ID = ID;
            //Answer = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Answer) ? "null" : Answer);
            //Question = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Question) ? "null" : Question);
            //Creator = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Creator) ? "null" : Creator);
            //Editor = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Editor) ? "null" : Editor);
            //URL = string.Format("{0}",
            //string.IsNullOrWhiteSpace(URL) ? "null" : URL);
            //Date_Created = string.Format("{0}",
            //string.IsNullOrWhiteSpace(Date_Created) ? "null" : Date_Created);


            return Page();
        }
    }

}