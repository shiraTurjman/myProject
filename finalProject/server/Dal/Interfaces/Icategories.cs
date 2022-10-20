using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Dal.Interfaces
{
    public interface Icategories
    {
        //שליפה רשימת הקטגוריות של משתמש ID 

        Task<List<CategoryEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

         Task<int> AddCategoryAsync(CategoryEntity c);

        //מחיקה

         Task<CategoryEntity> DeleteCategoryAsync(int categoryId);

        //עדכון

         Task<int> UpdateCategoryAsync(CategoryEntity category);
    }
}
