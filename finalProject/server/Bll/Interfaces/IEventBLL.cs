using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IEventBLL
    {
        //Add event for user 
        Task<int> AddEventAsync(EventEntity newEvent);

        //get list of all events for user 
        Task<List<EventEntity>> GetEventsForUserAsync(int userId);

        //Get an event by date
        Task<EventEntity> GetByDateAsync(DateTime d);

        //Delete by event id
        Task<int> DeleteByEventIdAsync(int eventId);

        //Update event by event id
        Task<int> UpdateEventAsync(EventEntity eventUp);
    }
}
