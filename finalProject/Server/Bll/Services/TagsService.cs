using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;


namespace Bll.Functions
{
    public class TagsService : ITagsService
    {
        private readonly ITagsRepository _ITagsRepository;
            
        public TagsService(ITagsRepository ITagsRepository)
        {            
            _ITagsRepository = ITagsRepository; 
        }
        public async Task AddTagAsync(TagEntity tag)
        {
            await _ITagsRepository.AddTagAsync(tag);
        }

        public async Task<TagEntity> DeleteTagByTagIdAsync(int tagId)
        {
            return await _ITagsRepository.DeleteTagByTagIdAsync(tagId);
        }

        public async Task<List<TagEntity>> GetAllByUserIdAsync(int userId)
        {
            return await _ITagsRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<int> UpdateTagAsync(TagEntity tag)
        {
            return await _ITagsRepository.UpdateTagAsync(tag);
        }

        public async Task<bool> CheckNameExist(string name)
        {
            return await _ITagsRepository.CheckNameExist(name);
        }
    }
}
