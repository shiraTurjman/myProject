using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
    public class TagConverter
    {
        //convert an object from dal to entity
        public static TagEntity toEntity(Tags t)
        {
            return new TagEntity {tagId=t.TagId, tagName=t.TagName, userId=t.UserId};
        }
        //convert an object from entity to dal
        public static Tags toDal(TagEntity t)
        {
            return new Tags { TagId=t.tagId, TagName=t.tagName, UserId=t.userId };

        }

        //convert a list of objects from dal to entity
        public static List<TagEntity> toListEntity(List<Tags> lt)
        {
            List<TagEntity> lte = new List<TagEntity>();
            foreach (var item in lt)
            {
                lte.Add(toEntity(item));
            }
            return lte;
        }

        //convert a list of objects from entity to dal

        public static List<Tags> toListDal(List<TagEntity> lte)
        {

            List<Tags> lt = new List<Tags>();
            foreach (var item in lte)
            {
                lt.Add(toDal(item));
            }
            return lt;
        }
    }
}
