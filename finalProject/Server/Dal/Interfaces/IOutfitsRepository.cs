using Dal.Entities;

namespace Dal.Interfaces
{
    public interface IOutfitsRepository
    {
        //שליפה רשימת הקטגוריות של משתמש ID 

        Task<List<OutfitEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

        Task<int> AddOutfitAsync(OutfitEntity c);

        //מחיקה

        Task DeleteOutfitAsync(int categoryId);

        //עדכון

        Task<int> UpdateOutfitAsync(OutfitEntity category);

        Task<OutfitEntity> GetByOutfitIdAsync(int outfitId);

    }
}
