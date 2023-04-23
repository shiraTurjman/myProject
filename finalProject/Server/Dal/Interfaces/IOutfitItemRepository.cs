
using Dal.Entities;

namespace Dal.Interfaces
{
   public interface IOutfitItemRepository
    {
        //Add outfitItem 
        Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem);

        //get all outfit by item id
        Task<List<OutfitItemEntity>> GetAllByItemIdAsync(int itemId);

        //get ouftit by outfitId
        Task<List<OutfitItemEntity>> GetAllByOutfitIdAsync(int outfitId);

        //delete a outfitItem by outfitItem id 
        Task DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId);

        //delete outfit מחיקת אוטפיט שלם... כל הפריטים ששיכים אליו

        Task DeleteByOutfitIdAsync(int outfitId);


        //delete OutfitItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        Task DeleteByItemIdAsync(int itemId);

    }
}
