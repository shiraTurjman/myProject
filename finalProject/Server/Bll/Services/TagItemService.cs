using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;


namespace Bll.Functions
{
    public class TagItemService : ITagItemService
    {
        private readonly ITagItemRepository _ITagItemRepository;
        public TagItemService(ITagItemRepository ITagItemRepository)
        {
           _ITagItemRepository = ITagItemRepository;    
        }

        public async Task AddTagItemAsync(TagItemEntity newTagItem)
        {
            await _ITagItemRepository.AddTagItemAsync(newTagItem);
        }

        public async Task DeleteByItemIdAsync(int itemId)
        {
            await _ITagItemRepository.DeleteByItemIdAsync(itemId);  
        }

        public async Task DeleteByTagIdAsync(int TagId)
        {
            await _ITagItemRepository.DeleteTagItemByTagItemIdAsync(TagId);
                
        }

        public async Task DeleteTagItemByTagItemIdAsync(int TagItemId)
        {
            await _ITagItemRepository.DeleteTagItemByTagItemIdAsync(TagItemId);
        }

        public async Task<List<TagEntity>> GetAllByItemIdAsync(int itemId)
        {
           return await _ITagItemRepository.GetAllByItemIdAsync(itemId);
        }

        public async Task<List<ItemEntity>> GetAllByTagIdAsync(int TagId)
        {
            return await _ITagItemRepository.GetAllByTagIdAsync(TagId);
        }

        
    }
}
