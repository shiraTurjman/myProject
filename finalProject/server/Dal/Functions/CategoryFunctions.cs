//פונקציה GETBYUSERID לא ממושת
//

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
    public class CategoryFunctions : Icategories
    {
        FinalProjectContext db;
        public CategoryFunctions(FinalProjectContext _db)
        {
            db = _db;
        }
        //add category
        public async Task<int> AddCategoryAsync(CategoryEntity c)
        {

            db.Categories.Add(CategoriesConverter.toDal(c));
            int x=await db.SaveChangesAsync();
            return x;

        }
        //delete category
        public async Task<CategoryEntity> DeleteCategoryAsync(int categoryId)
        {
            Categories c = db.Categories.Find(categoryId);

               db.Categories.Remove(c);

            int x = await db.SaveChangesAsync();
            return  CategoriesConverter.toEntity(c);
        }
        //get by  user id
        public async Task<List<CategoryEntity>> GetByUserIdAsync(int userId)
        {
            var list = await db.Categories.Where(c => c.UserId == userId).ToListAsync();
            return CategoriesConverter.toListEntity(list);
        }
        //update 
        public async Task<int> UpdateCategoryAsync(CategoryEntity category)
        {
            var categoryToUpdate = db.Categories.FirstOrDefault(category1 => category1.CategoryId == category.categoryId);
            if (categoryToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 

                categoryToUpdate.CategoryId = category.categoryId;
                categoryToUpdate.CategoryName = category.categoryName;
                categoryToUpdate.UserId = category.userId;
            }

            
            int x = await db.SaveChangesAsync();
            return x;
        }

        








        //add a category to current user
        //public void AddCategory(CategoryEntity c)
        //{
        //    try
        //    {
        //        db.Categories.Add(CategoriesConverter.toDal(c));
        //    }
        //    catch 
        //    { throw new Exception(); }
        //}

        //public Task<int> AddCategoryAsync(CategoryEntity c)
        //{
        //    throw new NotImplementedException();
        //}

        ////delete a category from current user
        //public CategoryEntity DeleteCategory(int categoryId)
        //{
        //    Categories c = db.Categories.Find(categoryId);

        //    db.Categories.Remove(c);
        //    db.SaveChanges();
        //    return CategoriesConverter.toEntity(c);
        //}

        //public Task<CategoryEntity> DeleteCategoryAsync(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}

        //// get all categories for current user
        //public List<CategoryEntity> GetById(int userId)
        //{//צריך טיפול

        //    //return CategoriesConvert.toListEntity(db.Categories.Find(x => x.UserId == userId));

        //    return null;

        //}

        //public Task<List<CategoryEntity>> GetByIdAsync(int userId)
        //{
        //    throw new NotImplementedException();
        //}

        ////update a certain category for current user
        //public CategoryEntity UpdateCategory(int categoryId)
        //{//צריך טיפול.........
        //    throw new NotImplementedException();
        //}

        //public Task<CategoryEntity> UpdateCategoryAsync(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
