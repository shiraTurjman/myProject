
using Dal.Entities;

namespace Dal.Interfaces
{
     public interface ITagsRepository
    {
        //return all tags for current user by id
        Task<List<TagEntity>> GetAllByUserIdAsync(int userId);

        //add a tag by user id
        Task AddTagAsync(TagEntity tag);

        //delete a tag by tag id 
        Task<TagEntity> DeleteTagByTagIdAsync(int tagId);

        //update a tag
        Task<int> UpdateTagAsync(TagEntity tag);

        Task<bool> CheckNameExist(string name);

    }
}
