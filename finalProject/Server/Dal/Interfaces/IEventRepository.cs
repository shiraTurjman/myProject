
using Dal.Entities;

namespace Dal.Interfaces
{
   public interface IEventRepository
    {
        //Add event for user 
        Task AddEventAsync(EventEntity newEvent);

        //get list of all events for user 
        Task<List<EventEntity>> GetEventsForUserAsync(int userId);

        //Get an event by date
        Task<EventEntity> GetByDateAsync(DateTime d, int userId);

        //Delete by event id
        Task DeleteByEventIdAsync(int eventId);

        //Update event by event id
        Task UpdateEventAsync(EventEntity eventUp);
    }
}
