
using Dal.Entities;

namespace Dal.Interfaces
{
    public interface IItemsRepository
    {
        //add item
        Task<int> AddItemAsync(ItemEntity item);

        //delete item 
        Task DeleteItemAsync(int itemId);

        //update item
        Task UpdateItemAsync(ItemEntity item);
        //get item dy user
        Task<List<ItemEntity>> GetByUserAsync(int userId);
        //get item by color
        Task<List<ItemEntity>> GetByColorAsync(int colorId, int userId);
        //get item by category
        Task<List<ItemEntity>> GetByCategoryAsync(int categoryId, int userId);
    }
}
