using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using Entities.Entities;

using Dal.Models;

namespace Dal.Functions
{
   public class TagItemFunction : ITagItem
    {
        FinalProjectContext db;
        public TagItemFunction(FinalProjectContext _db)
        {
            db = _db;
        }

        public async Task<int> AddTagItemAsync(TagItemEntity newTagItem)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> DeleteByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> DeleteByTagIdAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public async Task<TagItemEntity> DeleteTagItemByTagItemIdAsync(int tagItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> GetAllByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> GetAllByOutfitIdAsync(int tagId)
        {
            throw new NotImplementedException();
        }
    }
}
