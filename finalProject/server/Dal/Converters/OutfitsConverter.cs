using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
    public class OutfitsConverter
    {

        //convert an object from dal to entity
        public static OutfitEntity toEntity(OutfitsItems c)
        {
            return new OutfitEntity { outfitId = c.OutfitId, outfitName = c.OutfitName, userId = c.UserId };
        }
        //convert an object from entity to dal
        public static OutfitsItems toDal(OutfitEntity c)
        {
            return new OutfitsItems { OutfitId = c.outfitId, OutfitName = c.outfitName, UserId = c.userId };

        }

        //convert a list of objects from dal to entity
        public static List<OutfitEntity> toListEntity(List<OutfitsItems> lc)
        {
            List<OutfitEntity> lce = new List<OutfitEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<OutfitsItems> toListDal(List<OutfitEntity> lc)
        {

            List<OutfitsItems> lce = new List<OutfitsItems>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;

        }

    }
}
