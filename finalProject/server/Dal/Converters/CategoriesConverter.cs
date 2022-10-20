using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
    public class CategoriesConverter
    {
        //convert an object from dal to entity
        public static CategoryEntity toEntity(Categories c)
        {
            return new CategoryEntity { categoryId = c.CategoryId, categoryName = c.CategoryName, userId = c.UserId };
        }
        //convert an object from entity to dal
        public static Categories toDal(CategoryEntity c)
        {
            return new Categories { CategoryId = c.categoryId, CategoryName = c.categoryName, UserId = c.userId };

        }

        //convert a list of objects from dal to entity
        public static List<CategoryEntity> toListEntity(List<Categories> lc)
        { List<CategoryEntity> lce = new List<CategoryEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<Categories> toListDal(List<CategoryEntity> lc)
        {

            List<Categories> lce = new List<Categories>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;

        }

    }
}
