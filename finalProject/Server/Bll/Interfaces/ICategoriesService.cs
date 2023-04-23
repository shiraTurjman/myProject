using Dal.Entities;

namespace Bll.Interfaces
{
    public  interface ICategoriesService
    {

        //שליפה רשימת הקטגוריות של משתמש ID 

         Task<List<CategoryEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

        Task AddCategoryAsync(CategoryEntity c);

        //מחיקה

         Task<CategoryEntity> DeleteCategoryAsync(int categoryId);

        //עדכון
        
         Task UpdateCategoryAsync(CategoryEntity category);
        Task<bool> CheckNameExist(string name);
    }
}
