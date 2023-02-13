using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Events
{
    [Authorize(Roles = "Staff")]
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;
        public IndexModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Event> EventList { get; set; } = new();
        public Event MyEvent { get; set; } = new();

        public void OnGet()
        {
            EventList = _eventService.GetAll();
        }
        public async Task<IActionResult> OnGetDelete(string id)
        {
            if (id == null)
            {
                return Page();
            }

            var myEvent = _eventService.GetEventById(id);
            System.Diagnostics.Debug.WriteLine("await" + myEvent);

            if (myEvent != null)
            {
                if (myEvent.EventId.ToString() != id)
                {

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Invalid access");
                    return Page();
                }

                else
                {
                    _eventService.DeleteEvent(myEvent);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Event {0} is deleted", myEvent.EventName);
                }

            }

            return RedirectToPage();
        }
    }

}
