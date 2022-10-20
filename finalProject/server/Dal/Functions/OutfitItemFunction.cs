using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using Entities.Entities;
using Dal.Models;
using Dal.Converters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dal.Functions
{
    public class OutfitItemFunction : IOutfitItem
    {
        FinalProjectContext db;
        public OutfitItemFunction(FinalProjectContext _db)
        {
            db = _db;
        }

        public async Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem)
        {
            db.OutfitItems.Add(OutfitItemConverter.toDal(newOutfitItem));
           int res=await db.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteByItemIdAsync(int itemId)
        {
            OutfitItems outfitItemToDelete = db.OutfitItems.Find(itemId);
            db.OutfitItems.Remove(outfitItemToDelete);
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteByOutfitIdAsync(int outfitId)
        {
            OutfitItems outfitItemToDelete = db.OutfitItems.Find(outfitId);
            db.OutfitItems.Remove(outfitItemToDelete);
            int res = await db.SaveChangesAsync();
            return res;

        }

        public async Task<int> DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId)
        {
            OutfitItems outfitItemToDelete = db.OutfitItems.Find(outfitItemId);
            db.OutfitItems.Remove(outfitItemToDelete);
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<List<OutfitItemEntity>> GetAllByItemIdAsync(int itemId)
        {
            var list = await db.OutfitItems.Where(o=>o.ItemId==itemId).ToListAsync();
            return  OutfitItemConverter.toListEntity(list);
        }

        public async Task<List<OutfitItemEntity>> GetAllByOutfitIdAsync(int outfitId)
        {
            var list = await db.OutfitItems.Where(o => o.OutfitId == outfitId).ToListAsync();
            return OutfitItemConverter.toListEntity(list);
        }
    }
}
