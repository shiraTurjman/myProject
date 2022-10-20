
// GETלא כתוב ממישוש הפונקציות 

using Bll.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;

namespace Bll.Functions
{
    public class OutfitItemFuncBLL : IOutfitItemBLL
    {
        IOutfitItem dal;
        
        public OutfitItemFuncBLL(IOutfitItem _dal)
        {
            dal = _dal;

        }

        public async Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem)
        {
           return await dal.AddOutfitItemAsync(newOutfitItem);
        }

        public async Task<int> DeleteByItemIdAsync(int itemId)
        {
            return await dal.DeleteByItemIdAsync(itemId);
        }

        public async Task<int> DeleteByOutfitIdAsync(int outfitId)
        {
            return await dal.DeleteByOutfitIdAsync(outfitId);

        }

        public async Task<int> DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId)
        {
            return await dal.DeleteOutfitItemByOutfitItemIdAsync(outfitItemId);

        }
        //לסיים..... איך לגשת לפונקציה ממחלקה אחרת .....
        public async Task<List<OutfitEntity>> GetAllByItemIdAsync(int itemId)
        {
            var list = await dal.GetAllByItemIdAsync(itemId);
            List<OutfitEntity> outfitList = new List<OutfitEntity>();
            foreach(OutfitItemEntity item in list)
            {
              //  outfitList.Add()
            }

        }

        public async Task<List<ItemEntity>> GetAllByOutfitIdAsync(int outfitId)
        {
            throw new NotImplementedException();
        }
    }
}
