using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Bll.Interfaces
{
    public interface IOutfitsBLL
    {

        Task<List<OutfitEntity>> GetByUserIdAsync(int userId);

        //הוספת קטגוריה למשתמש מסויים  

        Task<int> AddOutfitAsync(OutfitEntity c);

        //מחיקה

        Task<OutfitEntity> DeleteoutfitAsync(int categoryId);

        //עדכון

        Task<int> UpdateOutfitAsync(OutfitEntity category);
    }
}
