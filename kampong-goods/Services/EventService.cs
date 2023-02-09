using kampong_goods.Models;

namespace kampong_goods.Services
{
    public class EventService
    {
        private readonly EventDbContext _context;

        public EventService(EventDbContext context)
        {
            _context = context;
        }

        public List<Event> GetAll()
        {
            return _context.Events.OrderBy(e => e.EventId).ToList();
        }

        public Event? GetEventById(string id)
        {
            return _context.Events.FirstOrDefault(e => e.EventId == id);
        }

        public void AddEvent(Event myEvent)
        {
            _context.Events.Add(myEvent);
            _context.SaveChanges();
        }

        public void UpdateEvent(Event myEvent)
        {
            _context.Events.Update(myEvent);
            _context.SaveChanges();
        }

        public void DeleteEvent(Event myEvent)
        {
            _context.Events.Remove(myEvent);
            _context.SaveChanges();
        }

    }
}
