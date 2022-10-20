using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
   public class EventConverter
    {
        //convert an object from dal to entity
        public static EventEntity toEntity(Event e)
        {
            return new EventEntity { eventId=e.EventId, itemId=e.ItemId, dateEvent=e.DateEvent, outfitId=e.OutfitId ,userId=e.UserId};
        }
        //convert an object from entity to dal
        public static Event toDal(EventEntity e)
        {
            return new Event { EventId = e.eventId, ItemId = e.itemId, DateEvent = e.dateEvent, OutfitId = e.outfitId,UserId=e.userId };

        }

        //convert a list of objects from dal to entity
        public static List<EventEntity> toListEntity(List<Event> le)
        {
            List<EventEntity> leEntity = new List<EventEntity>();
            foreach (var item in le)
            {
                leEntity.Add(toEntity(item));
            }
            return leEntity;
        }

        //convert a list of objects from entity to dal

        public static List<Event> toListDal(List<EventEntity> lee)
        {

            List<Event> lEvent = new List<Event>();
            foreach (var item in lee)
            {
                lEvent.Add(toDal(item));
            }
            return lEvent;
        }
    }
}
