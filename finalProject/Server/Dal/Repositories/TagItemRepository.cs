
using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
   public class TagItemRepository : ITagItemRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public TagItemRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory; 
        }

        public async Task<int> AddTagItemAsync(TagItemEntity newTagItem)
        {
            using var context= _factory.CreateDbContext();
            await context.TagItems.AddAsync(newTagItem);
            return await context.SaveChangesAsync();   
        }

        public async Task DeleteByItemIdAsync(int itemId)
        {
            using var context = _factory.CreateDbContext();
            TagItemEntity itemToDelete = await context.TagItems.Where(t=>t.ItemId==itemId).FirstOrDefaultAsync();
            if (itemToDelete != null)
            {
                context.TagItems.Remove(itemToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }

        }

        public async Task DeleteByTagIdAsync(int tagId)
        {
            using var context = _factory.CreateDbContext();
            List<TagItemEntity> itemToDelete = await context.TagItems.Where(t => t.TagId == tagId).ToListAsync();
            if (itemToDelete != null)
            {
                foreach (var item in itemToDelete)
                {
                    context.TagItems.Remove(item);
                }
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }
        }

        public async Task DeleteTagItemByTagItemIdAsync(int tagItemId)
        {
            using var context = _factory.CreateDbContext();
            List<TagItemEntity> itemToDelete = await context.TagItems.Where(t => t.TagItemId == tagItemId).ToListAsync();
            if (itemToDelete != null)
            {
                foreach(var item in itemToDelete)
                {
                    context.TagItems.Remove(item);
                }
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }
        }

        public async Task<List<TagEntity>> GetAllByItemIdAsync(int itemId)
        {
            using var context= _factory.CreateDbContext();
            List<TagItemEntity> result = await context.TagItems.Where(t => t.ItemId == itemId).ToListAsync();
            List<TagEntity> tags = new List<TagEntity>();
            foreach (var item in result)
            {
                tags.Append(await context.Tags.FirstOrDefaultAsync(i => i.TagId == item.TagId));
            }
            return tags;
        }

        public async Task<List<TagItemEntity>> GetAllByOutfitIdAsync(int tagId)
        {
            using var context = _factory.CreateDbContext();
            List<TagItemEntity> result = await context.TagItems.Where(t => t.TagId == tagId).ToListAsync();
            return result;
        }

        public async Task<List<ItemEntity>> GetAllByTagIdAsync(int tagId)
        {
            using var context=_factory.CreateDbContext();
            List<TagItemEntity> result=await context.TagItems.Where(t => t.TagId == tagId).ToListAsync();
            List<ItemEntity> items = new List<ItemEntity>();
            foreach(var item in result)
            {
              items.Append(await context.Items.FirstOrDefaultAsync(i => i.ItemId == item.ItemId));
            }
            return items;           
        }
    }
}
