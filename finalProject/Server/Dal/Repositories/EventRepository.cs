//GetEventsForUseAsyncr לא מומש 

using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;

        public EventRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }

        public async Task AddEventAsync(EventEntity newEvent)
        {
            using var context = _factory.CreateDbContext();
            context.Events.Add(newEvent);
            await context.SaveChangesAsync();
        }


        public async Task DeleteByEventIdAsync(int eventId)
        {
            using var context = _factory.CreateDbContext();
            EventEntity eventToDelete = await context.Events.Where(e => e.EventId == eventId).FirstOrDefaultAsync();
            if (eventToDelete != null)
            {
                context.Events.Remove(eventToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Event not found");
            }

            // do we need to return the event that we deleted???
        }

        //take care of this function שולף את כולם???  DATETIME זה משתנה עם שעה??? 
        public async Task<EventEntity> GetByDateAsync(DateTime d, int userId)
        {
            using var context = _factory.CreateDbContext();
            var eventToUpdate = context.Events.FirstOrDefault(e => e.DateEvent == d && e.UserId == userId);
            if (eventToUpdate != null)
                return (eventToUpdate);
            else
                throw new Exception("Event not found");
        }

        public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Events.Where(e => e.UserId == userId).ToListAsync();
            return list;
        }


        public async Task UpdateEventAsync(EventEntity eventUp)
        {
            using var context = _factory.CreateDbContext();


            var eventToUpdate = context.Events.FirstOrDefault(e => e.EventId == eventUp.EventId);
            if (eventToUpdate != null)
            {
                //לעשות המרה לבד כדי לא לאבד מצביע 
                eventToUpdate.EventId = eventUp.EventId;
                eventToUpdate.ItemId = eventUp.ItemId;
                eventToUpdate.OutfitId = eventUp.OutfitId;
                eventToUpdate.DateEvent = eventUp.DateEvent;
                eventToUpdate.UserId = eventUp.UserId;
            }
            await context.SaveChangesAsync();
        }
    }
}
