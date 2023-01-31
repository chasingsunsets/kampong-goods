using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kampong_goods.Models;
using kampong_goods.Services;
using Microsoft.AspNetCore.Authorization;

namespace kampong_goods.Pages.Events
{
    public class IndexModel : PageModel
    {
        public List<Event> EventList { get; set; } = new()
        {
            new Event
            {
                EventId = "4",
                EventName = "DonateAgain",
                EventDesc = "Donate",
                EventType = "Donation",
                EventDate = DateTime.Parse("14/02/2023"),
                EventTime = DateTime.Parse("09:30"),
                EventLocation = "Jurong",
                PostalCode = "123456",
                EventSuitability = "Experienced",
                EventOrganiser = "PAP"
            },
            new Event
            {
                EventId = "4",
                EventName = "DonateAgain",
                EventDesc = "Donate",
                EventType = "Donation",
                EventDate = DateTime.Parse("14/02/2023"),
                EventTime = DateTime.Parse("09:30"),
                EventLocation = "Jurong",
                PostalCode = "123456",
                EventSuitability = "Experienced",
                EventOrganiser = "PAP"
            },
            new Event
            {
                EventId = "4",
                EventName = "DonateAgain",
                EventDesc = "Donate",
                EventType = "Donation",
                EventDate = DateTime.Parse("14/02/2023"),
                EventTime = DateTime.Parse("09:30"),
                EventLocation = "Jurong",
                PostalCode = "123456",
                EventSuitability = "Experienced",
                EventOrganiser = "PAP"
            },
            new Event
            {
                EventId = "4",
                EventName = "DonateAgain",
                EventDesc = "Donate",
                EventType = "Donation",
                EventDate = DateTime.Parse("14/02/2023"),
                EventTime = DateTime.Parse("09:30"),
                EventLocation = "Jurong",
                PostalCode = "123456",
                EventSuitability = "Experienced",
                EventOrganiser = "PAP"
            },
        };

        public void OnGet()
        {
        }
    }
 }
