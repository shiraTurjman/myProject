using Bll.Interfaces;
using Dal.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class TagItemFuncBLL : ITagItemBLL
    {
        ITagItem dal;
        public TagItemFuncBLL(ITagItem _dal)
        {
            dal = _dal;

        }

        public async Task<int> AddTagItemAsync(TagItemEntity newTagItem)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> DeleteByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagItemEntity>> DeleteByTagIdAsync(int TagId)
        {
            throw new NotImplementedException();
        }

        public async Task<TagItemEntity> DeleteTagItemByTagItemIdAsync(int TagItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TagEntity>> GetAllByItemIdAsync(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ItemEntity>> GetAllByTagIdAsync(int TagId)
        {
            throw new NotImplementedException();
        }
    }
}
