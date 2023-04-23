using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dal.Functions
{
    public class OutfitsRepository : IOutfitsRepository
    {

        private readonly IDbContextFactory<ServerDBContext> _factory;
        public OutfitsRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }
        public async Task<int> AddOutfitAsync(OutfitEntity outfit)
        {
            using var context = _factory.CreateDbContext();
            await context.Outfits.AddAsync(outfit);
            return await context.SaveChangesAsync();
   
        }

        public async Task<List<OutfitEntity>> GetByUserIdAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var list= await context.Outfits.Where(x => x.UserId == userId).ToListAsync();
            return list;
        }

        public async Task<int> UpdateOutfitAsync(OutfitEntity outfit)
        {
            throw new NotImplementedException();
        }
        public async Task<OutfitEntity> GetByOutfitIdAsync(int outfitId) 
        {
            using var context=_factory.CreateDbContext();
            OutfitEntity outfit= await context.Outfits.FindAsync(outfitId);
            if (outfit == null)
            {
                throw new Exception("Outfir not found");
            }
            return outfit;
        }

        public async Task DeleteOutfitAsync(int outfitId)
        {
           using var context=_factory.CreateDbContext();
            OutfitEntity outfitToDelete = await context.Outfits.FindAsync(outfitId);
            if (outfitToDelete != null)
            {
                context.Outfits.Remove(outfitToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Could not delete outfit");
            }
        }
    }
}
