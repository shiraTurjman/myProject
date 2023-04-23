

using Dal.Entities;

namespace Bll.Interfaces
{
    public interface IOutfitService
    {

        Task<List<OutfitEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

        Task<int> AddOutfitAsync(OutfitEntity c);

        //מחיקה

        Task DeleteOutfitAsync(int outfitId);

        //עדכון

        Task<int> UpdateOutfitAsync(OutfitEntity category);
    }
}
