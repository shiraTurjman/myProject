
using Dal.Entities;
using Dto;

namespace Bll.Interfaces
{
    public interface IItemService
    {
        //add item
        Task<int> AddItemAsync(AddItemDto item);

        //delete item 
        Task DeleteItemAsync(int itemId);

        //update item
        Task UpdateItemAsync(ItemEntity item);
        //get item dy user
        Task<List<ItemDto>> GetByUserAsync(int userId);
        //get item by color
        Task<List<ItemDto>> GetByColorAsync(int colorId, int userId);
        //get item by category
        Task<List<ItemDto>> GetByCategoryAsync(int cateroryId, int userId);

    }
}
