using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public  interface ICategoriesBLL
    {

        //שליפה רשימת הקטגוריות של משתמש ID 

         Task<List<CategoryEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

        Task<int> AddCategoryAsync(CategoryEntity c);

        //מחיקה

         Task<CategoryEntity> DeleteCategoryAsync(int categoryId);

        //עדכון
        
         Task<int> UpdateCategoryAsync(CategoryEntity categoryId);
    }
}
