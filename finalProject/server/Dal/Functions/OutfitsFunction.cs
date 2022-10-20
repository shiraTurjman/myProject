using Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;
using Dal.Converters;
using System.Threading.Tasks;

namespace Dal.Functions
{
    public class OutfitsFunction : IOutfits
    {

        FinalProjectContext db;
        public OutfitsFunction(FinalProjectContext _db)

        {
            db = _db;

        }
        public async Task<int> AddOutfitAsync(OutfitEntity outfit)
        {
            db.Outfits.Add(OutfitsConverter.toDal(outfit));
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<OutfitEntity> DeleteoutfitAsync(int categoryId,int usetId)
        {

            outfits outfitItemToDelete = db.OutfitItems.Find(outfitId);
            db.OutfitItems.Remove(outfitItemToDelete);
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<List<OutfitEntity>> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateOutfitAsync(OutfitEntity category)
        {
            throw new NotImplementedException();
        }
        public async Task<List<OutfitEntity>> GetByOutfitIdAsync(int outfitId) 
        {
            throw new NotImplementedException();
        }
    }
}
