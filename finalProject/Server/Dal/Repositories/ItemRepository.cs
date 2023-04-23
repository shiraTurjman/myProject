
using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class ItemRepository : IItemsRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;

        public ItemRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }

        public async Task<int> AddItemAsync(ItemEntity item)
        {
            using var context = _factory.CreateDbContext();
            context.Items.Add(item);
            await context.SaveChangesAsync();
            var temp = await context.Items.FirstOrDefaultAsync(i => i.ItemId == item.ItemId);
            return temp.ItemId;
        }
        //delete

        public async Task DeleteItemAsync(int itemId)
        {
            using var context = _factory.CreateDbContext();
            ItemEntity itemToDelete = await context.Items.FindAsync(itemId);
            if (itemToDelete != null)
            {
                context.Items.Remove(itemToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item not found");
            }
        }


        public async Task<List<ItemEntity>> GetByCategoryAsync(int categoryId, int userId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Items.Where(i => i.CategoryId == categoryId && i.UserId == userId).ToListAsync();
            return list;
        }

        public async Task<List<ItemEntity>> GetByColorAsync(int colorId, int userId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Items.Where(i => i.ColorId == colorId && i.UserId == userId).ToListAsync();
            return list;
        }

        public async Task<List<ItemEntity>> GetByUserAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Items.Where(i => i.UserId == userId).ToListAsync();
            if (list != null)
            {
                return list;
            }
            throw new Exception("The user you selected doesn't have any items");
        }

        public async Task UpdateItemAsync(ItemEntity item)
        {
            using var context = _factory.CreateDbContext();
            var itemToUpdate = context.Items.FirstOrDefault(item1 => item1.ItemId == item.ItemId);
            if (itemToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 
                itemToUpdate.ItemId = item.ItemId;
                itemToUpdate.CategoryId = item.CategoryId;
                // if (item.color != null)
                // { itemToUpdate.Color = item.color; }
                itemToUpdate.EntryDate = item.EntryDate;
                itemToUpdate.ColorId = item.ColorId;
                itemToUpdate.Img = item.Img;
                itemToUpdate.UserId = item.UserId;
            }
            await context.SaveChangesAsync();
        }

    }
}
