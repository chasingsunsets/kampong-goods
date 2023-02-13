using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;

namespace kampong_goods.Pages.Events
{
    public class EventCatalogueModel : PageModel
    {
        private readonly EventService _eventService;

        public EventCatalogueModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Event> EventList { get; set; } = new();
        public Event MyEvent { get; set; } = new();


        public void OnGet()
        {
            EventList = _eventService.GetAll();
        }
    }
}