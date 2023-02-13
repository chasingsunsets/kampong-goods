using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Events
{
    [Authorize(Roles = "Customer, Staff")]

    public class EditEventModel : PageModel
    {
        private readonly EventService _eventService;
        public EditEventModel(EventService eventService)
        { _eventService = eventService; }

        [BindProperty]
        public Event MyEvent { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Event? myEvent = _eventService.GetEventById(id);
            if (myEvent != null)
            {
                MyEvent = myEvent;
                return Page();
            }
            else
            {
                return Redirect("/Events");
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _eventService.UpdateEvent(MyEvent);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} is updated", MyEvent.EventName);
                return Redirect("/Events");
            }
            return Page();
        }
    }
}
