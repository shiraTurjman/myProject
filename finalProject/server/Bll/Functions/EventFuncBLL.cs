using System;
using System.Collections.Generic;
using System.Text;
using Bll.Interfaces;
using Entities.Entities;
using Dal.Interfaces;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class EventFuncBLL : IEventBLL
    {
        IEvent dal;
        public EventFuncBLL(IEvent _dal)
        {
            dal = _dal;
        }

        public async Task<int> AddEventAsync(EventEntity newEvent)
        {
            
           return await dal.AddEventAsync(newEvent);
        }

        public async Task<int> DeleteByEventIdAsync(int eventId)
        {
           return await dal.DeleteByEventIdAsync(eventId);
        }

        public async Task<EventEntity> GetByDateAsync(DateTime d)
        {
          return await dal.GetByDateAsync(d);
        }

        public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        {
            
          //  throw new NotImplementedException();
            return await dal.GetEventsForUserAsync(userId);
        }

        //public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        //{
        //    return await dal.GetEventsForUserAsync(userId);
        //}

        public async Task<int> UpdateEventAsync(EventEntity eventUp)
        {
            return await dal.UpdateEventAsync(eventUp);
        }
    }
}
