using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kampong_goods.Pages.Education
{
    public class HelpCentreModel : PageModel
    {
        private readonly FAQService _FAQService;
        public FAQ myFAQ { get; set; } = new();

        public List<FAQ> FAQlist { get; set; } = new();
        public HelpCentreModel(FAQService faqService)
        {
            _FAQService = faqService;
        }

        public void OnGet()
        {
            FAQlist = _FAQService.GetAll();

        }
    }
}
