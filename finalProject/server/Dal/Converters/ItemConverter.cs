using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
   public class ItemConverter
    {
        //convert an object from dal to entity
        public static ItemEntity toEntity(Items c)
        {
            return new ItemEntity {itemId=c.ItemId, userId=c.UserId, categoryId=c.CategoryId,  entryDate=c.EntryDate, img=c.Img, color=c.ColorId};
        }
        //convert an object from entity to dal
        public static Items toDal(ItemEntity c)
        {
            return new Items { ItemId = c.itemId, UserId = c.userId, CategoryId = c.categoryId, EntryDate = c.entryDate, Img = c.img, ColorId = c.color };

        }

        //convert a list of objects from dal to entity
        public static List<ItemEntity> toListEntity(List<Items> lc)
        {
            List<ItemEntity> lce = new List<ItemEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<Items> toListDal(List<ItemEntity> lc)
        {

            List<Items> lce = new List<Items>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;
        }
    }
}
