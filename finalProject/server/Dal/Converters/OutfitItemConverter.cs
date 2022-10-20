using System;
using System.Collections.Generic;
using System.Text;
using Dal.Models;
using Entities.Entities;

namespace Dal.Converters
{
   public class OutfitItemConverter
    {
        //convert an object from dal to entity
        public static OutfitItemEntity toEntity(OutfitItems c)
        {
            return new OutfitItemEntity { OutfitItemId=c.OutfitItemId, OutfitId=c.OutfitId, ItemId=c.ItemId };
        }
        //convert an object from entity to dal

        public static OutfitItems toDal(OutfitItemEntity c)
        {
            return new OutfitItems { OutfitId = c.OutfitId, OutfitItemId=c.OutfitItemId, ItemId=c.ItemId };

        }

        //convert a list of objects from dal to entity
        public static List<OutfitItemEntity> toListEntity(List<OutfitItems> lc)
        {
            List<OutfitItemEntity> lce = new List<OutfitItemEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<OutfitItems> toListDal(List<OutfitItemEntity> lc)
        {

            List<OutfitItems> lce = new List<OutfitItems>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;

        }

    }
}
