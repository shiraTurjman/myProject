

using Dal.Entities;

namespace Bll.Interfaces
{
    public interface IOutfitItemService
    {
        //Add outfitItem 
        Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem);

        //get all outfit by item id שליפת כל האוטפיטים שפריט שייך אליהם
        Task<List<OutfitEntity>> GetAllByItemIdAsync(int itemId);

        //get ouftit by outfitId שליפת אוטפיט שלם 
        Task<List<ItemEntity>> GetAllByOutfitIdAsync(int outfitId);

        //delete a outfitItem  by outfitItem id 
        Task DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId);

        //delete outfit מחיקת אוטפיט שלם... כל הפריטים ששיכים אליו

        Task DeleteByOutfitIdAsync(int outfitId);


        //delete OutfitItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        Task DeleteByItemIdAsync(int itemId);

    }

}
