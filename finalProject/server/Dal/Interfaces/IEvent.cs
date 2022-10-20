using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Dal.Interfaces
{
   public interface IEvent
    {
        //Add event for user 
        Task<int> AddEventAsync(EventEntity newEvent);

        //get list of all events for user 
        Task<List<EventEntity>> GetEventsForUserAsync(int userId);

        //Get an event by date
        Task<EventEntity> GetByDateAsync(DateTime d,int userId);

        //Delete by event id
        Task<int> DeleteByEventIdAsync(int eventId);

        //Update event by event id
        Task<int> UpdateEventAsync(EventEntity eventUp);
    }
}
