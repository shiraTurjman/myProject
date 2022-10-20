using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IOutfitItemBLL
    {
        //Add outfitItem 
        Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem);

        //get all outfit by item id שליפת כל האוטפיטים שפריט שייך אליהם
        Task<List<OutfitEntity>> GetAllByItemIdAsync(int itemId);

        //get ouftit by outfitId שליפת אוטפיט שלם 
        Task<List<ItemEntity>> GetAllByOutfitIdAsync(int outfitId);

        //delete a outfitItem  by outfitItem id 
        Task<int> DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId);

        //delete outfit מחיקת אוטפיט שלם... כל הפריטים ששיכים אליו

        Task<int> DeleteByOutfitIdAsync(int outfitId);


        //delete OutfitItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        Task<int> DeleteByItemIdAsync(int itemId);

    }

}
