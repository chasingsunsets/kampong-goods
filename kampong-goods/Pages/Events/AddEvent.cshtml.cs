using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;

namespace kampong_goods.Pages.Events
{
    public class AddEventModel : PageModel
    {
        private readonly EventService _eventService;

        public AddEventModel(EventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public Event MyEvent { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Event? myEvent = _eventService.GetEventById(MyEvent.EventId);
                if(myEvent != null)
                {
                    ModelState.AddModelError("MyEvent.EventId", "Event ID already exists.");
                    return Page();
                }

                _eventService.AddEvent(MyEvent);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is added", MyEvent.EventName);
                return Redirect("/Events/Index");
            }
            return Page();
        }
    }
}
