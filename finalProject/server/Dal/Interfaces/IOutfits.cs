using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IOutfits
    {
        
        

            //שליפה רשימת הקטגוריות של משתמש ID 

            Task<List<OutfitEntity>> GetByUserIdAsync(int userId);

            //הוספת קטגוריה למשתמש מסויים  

            Task<int> AddOutfitAsync(OutfitEntity c);

            //מחיקה

            Task<OutfitEntity> DeleteoutfitAsync(int categoryId);

            //עדכון

            Task<int> UpdateOutfitAsync(OutfitEntity category);

           Task<List<OutfitEntity>> GetByOutfitIdAsync(int outfitId);

    }
}
