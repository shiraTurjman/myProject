using System;
using System.Collections.Generic;
using System.Text;
using Bll.Interfaces;
using Entities.Entities;
using Dal.Interfaces;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class ItemFuncBLL : IItemBLL
    {

        IItems dal;
        public ItemFuncBLL(IItems _dal)
        {
            dal=_dal;
                
        }

        public async Task<int> AddItemAsync(ItemEntity item)
        {
             return await dal.AddItemAsync(item);
        }

        public async Task<int> DeleteItemAsync(int itemId)
        {
            return await dal.DeleteItemAsync(itemId);
        }

        public async Task<List<ItemEntity>> GetByCategoryAsync(int cateroryId, int userId)
        {
            return await dal.GetByCategoryAsync(cateroryId, userId);
        }

        public async Task<List<ItemEntity>> GetByColorAsync(int colorId, int userId)
        {
            return await  dal.GetByColorAsync(colorId, userId);
        }

        public async Task<List<ItemEntity>> GetByUserAsync(int userId)
        {

            return await dal.GetByUserAsync(userId);
        }

        public async Task<int> UpdateItemAsync(ItemEntity item)
        {
            return await dal.UpdateItemAsync(item);
                 
        }
    }
}
