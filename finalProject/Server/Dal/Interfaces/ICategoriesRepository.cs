

using Dal.Entities;

namespace Dal.Interfaces
{
    public interface ICategoriesRepository
    {

        //הוספת קטגוריה למשתמש מסויים  

        Task AddCategoryAsync(CategoryEntity category);

        //שליפה רשימת הקטגוריות של משתמש ID 

        Task<List<CategoryEntity>> GetByUserIdAsync(int userId);

        //מחיקה

        Task<CategoryEntity> DeleteCategoryAsync(int categoryId);

        //עדכון

        Task UpdateCategoryAsync(CategoryEntity category);

        Task<bool> CheckNameExist(string name);
    }
}
