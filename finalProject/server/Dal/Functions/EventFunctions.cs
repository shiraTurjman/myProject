//GetEventsForUseAsyncr לא מומש 

using System;
using System.Collections.Generic;
using System.Text;
using Dal.Interfaces;
using Dal.Models;
using Entities.Entities;
using Dal.Converters;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class EventFunctions : IEvent
    {
        FinalProjectContext db;
        public EventFunctions(FinalProjectContext _db)
        {
            db = _db;
        }

        public async Task<int> AddEventAsync(EventEntity newEvent)
        {
           
            db.Event.Add(EventConverter.toDal(newEvent));
            int x = await db.SaveChangesAsync();
            return x;
        }


        public async Task<int> DeleteByEventIdAsync(int eventId)
        {
            Event eventToDelete = db.Event.Find(eventId);
            db.Event.Remove(eventToDelete);
            int x= await db.SaveChangesAsync();
            return x;
            // do we need to return the event that we deleted???

        }
        //take care of this function שולף את כולם???  DATETIME זה משתנה עם שעה??? 
        public async Task<EventEntity> GetByDateAsync(DateTime d,int userId)
        {
            var eventToUpdate = db.Event.FirstOrDefault(event1 => event1.DateEvent == d && event1.UserId==userId);
            if (eventToUpdate != null)
            { return EventConverter.toEntity(eventToUpdate); }
            
             return null;
        }

        public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        {
              var list=await db.Event.Where(e => e.UserId == userId).ToListAsync();

              return EventConverter.toListEntity(list);

        }

        //public async Task<List<EventEntity>> GetEventsForUserAsync(int userId)
        //{// how do we get the events for the user if there is no field userid?
        //    List<Event> listEvents = new List<Event>();
        //    foreach(var item in db.Event)
        //    {
        //       // if(item.user)
        //    }
        //    return EventConverter.toListEntity(listEvents);

        //}

        public async Task<int> UpdateEventAsync(EventEntity eventUp)
        {


            var eventToUpdate = db.Event.FirstOrDefault(event1 =>event1.EventId== eventUp.eventId);
            if (eventToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 

                eventToUpdate.EventId = eventUp.eventId;
                eventToUpdate.ItemId = eventUp.itemId;
                eventToUpdate.OutfitId = eventUp.outfitId;
                eventToUpdate.DateEvent = eventUp.dateEvent;
                eventToUpdate.UserId = eventUp.userId;
            }


            int x = await db.SaveChangesAsync();
            return x;
        }
        }
}
