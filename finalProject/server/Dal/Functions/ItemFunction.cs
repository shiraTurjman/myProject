
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Interfaces;
using Dal.Converters;
using Dal.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class ItemFunction : IItems
    {
        FinalProjectContext db;

        public ItemFunction(FinalProjectContext _db)
        {
            db = _db;
        }

        public async Task<int> AddItemAsync(ItemEntity item)
        {
            db.Items.Add(ItemConverter.toDal(item));
            int x = await db.SaveChangesAsync();
            return x;
        }
        //delete

        public async Task<int> DeleteItemAsync(int itemId)
        {
            Items itemToDelete = db.Items.Find(itemId);
            db.Items.Remove(itemToDelete);
            int x = await db.SaveChangesAsync();
            return x;
        }

        public async Task<List<ItemEntity>> GetByCategoryAsync(int categoryId,int userId)
        {
            var list = await db.Items.Where(i => i.CategoryId == categoryId&&i.UserId==userId).ToListAsync();
            return ItemConverter.toListEntity(list);
        }

        public async Task<List<ItemEntity>> GetByColorAsync(int colorId, int userId)
        {   
            var list = await db.Items.Where(i => i.ColorId == colorId && i.UserId == userId).ToListAsync();
            return ItemConverter.toListEntity(list);
        }

        public async Task<List<ItemEntity>> GetByUserAsync(int userId)
        {
            var list = await db.Items.Where(i => i.UserId == userId).ToListAsync();
            return ItemConverter.toListEntity(list);
        }

        public async Task<int> UpdateItemAsync(ItemEntity item)
        {

            var itemToUpdate = db.Items.FirstOrDefault(item1 => item1.ItemId == item.itemId);
            if (itemToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 

                itemToUpdate.ItemId = item.itemId;
                itemToUpdate.CategoryId = item.categoryId;
                // if (item.color != null)
                // { itemToUpdate.Color = item.color; }
                itemToUpdate.EntryDate = item.entryDate;
                itemToUpdate.ColorId = item.color;
                itemToUpdate.Img = item.img;
                itemToUpdate.UserId = item.userId;
            }
            

            int x = await db.SaveChangesAsync();
            return x;
        }
        




    }
}
