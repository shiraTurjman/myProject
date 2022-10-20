using Dal.Models;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Converters
{
   public class TagItemConverter
    {
        //convert an object from dal to entity
        public static TagItemEntity toEntity(TagItem c)
        {
            return new TagItemEntity { TagItemId = c.TagItemId, TagId = c.TagId, ItemId = c.ItemId };
        }
        //convert an object from entity to dal

        public static TagItem toDal(TagItemEntity c)
        {
            return new TagItem { TagId = c.TagId , TagItemId = c.TagItemId, ItemId = c.ItemId };

        }

        //convert a list of objects from dal to entity
        public static List<TagItemEntity> toListEntity(List<TagItem> lc)
        {
            List<TagItemEntity> lce = new List<TagItemEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<TagItem> toListDal(List<TagItemEntity> lc)
        {

            List<TagItem> lce = new List<TagItem>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;

        }

    }
}
