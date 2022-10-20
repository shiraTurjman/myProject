using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
namespace Bll.Interfaces
{
    public interface IItemBLL
    {
        //add item
        Task<int> AddItemAsync(ItemEntity item);

        //delete item 
        Task<int> DeleteItemAsync(int itemId);

        //update item
        Task<int> UpdateItemAsync(ItemEntity item);
        //get item dy user
        Task<List<ItemEntity>> GetByUserAsync(int userId);
        //get item by color
        Task<List<ItemEntity>> GetByColorAsync(int colorId, int userId);
        //get item by category
        Task<List<ItemEntity>> GetByCategoryAsync(int cateroryId, int userId);

    }
}
