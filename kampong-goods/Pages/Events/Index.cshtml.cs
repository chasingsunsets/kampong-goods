using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;
        public IndexModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Event> EventList { get; set; } = new();

        public void OnGet()
        {
            EventList = _eventService.GetAll();
        }
    }
 }
