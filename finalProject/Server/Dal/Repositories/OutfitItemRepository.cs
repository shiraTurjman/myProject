using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class OutfitItemRepository : IOutfitItemRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public OutfitItemRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }

        public async Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem)
        {
            using var context = _factory.CreateDbContext();
            await context.OutfitItems.AddAsync(newOutfitItem);
            return await context.SaveChangesAsync();

        }

        public async Task DeleteByItemIdAsync(int itemId)
        {
            using var context = _factory.CreateDbContext();
            OutfitItemEntity outfitItemToDelete = await context.OutfitItems.FindAsync(itemId);
            if (outfitItemToDelete != null)
            {
                context.Remove(outfitItemToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Could not delete.");
            }
        }

        public async Task DeleteByOutfitIdAsync(int outfitId)
        {
            using var context = _factory.CreateDbContext();
            List<OutfitItemEntity> outfitItemToDelete = await context.OutfitItems.Where(i => i.OutfitId == outfitId).ToListAsync() ;
            if (outfitItemToDelete != null)
            {
                foreach (var item in outfitItemToDelete)
                {
                    context.Remove(item);
                }
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }

        }

        public async Task DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId)
        {
            using var context = _factory.CreateDbContext();
            OutfitItemEntity outfitItemToDelete = await context.OutfitItems.FindAsync(outfitItemId);
            if (outfitItemToDelete != null)
            {
                context.Remove(outfitItemToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }
        }

        public async Task<List<OutfitItemEntity>> GetAllByItemIdAsync(int itemId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.OutfitItems.Where(o => o.ItemId == itemId).ToListAsync();
            return list;
        }

        public async Task<List<OutfitItemEntity>> GetAllByOutfitIdAsync(int outfitId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.OutfitItems.Where(o => o.OutfitId == outfitId).ToListAsync();
            return list;
        }
    }
}
