using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Dal.Functions
{
    public class TagRepository : ITagsRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public TagRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddTagAsync(TagEntity tag)
        {
            using var context = _factory.CreateDbContext();
            await context.Tags.AddAsync(tag);
            await context.SaveChangesAsync();
        }

        public async Task<TagEntity> DeleteTagByTagIdAsync(int tagId)
        {
            using var context= _factory.CreateDbContext();
            var tagToDelete= await context.Tags.FirstOrDefaultAsync(t=>t.TagId == tagId);
            if (tagToDelete == null)
                throw new Exception("Couldn't delete tag because it didn't exist.");
            context.Remove(tagToDelete);
            await context.SaveChangesAsync();
            return tagToDelete;
           
        }

        public async Task<List<TagEntity>> GetAllByUserIdAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            List<TagEntity> result = await context.Tags.Where(t => t.UserId == userId).ToListAsync();
            return result;
        }

        public async Task<int> UpdateTagAsync(TagEntity tag)
        {
            using var context = _factory.CreateDbContext();
            var tagToUpdate = await context.Tags.FirstOrDefaultAsync(t => t.UserId == tag.UserId);
            if (tagToUpdate == null)
                throw new Exception("Couldn't update tag.");
            tagToUpdate.TagName = tag.TagName;
            tagToUpdate.UserId = tag.UserId;
            int x = await context.SaveChangesAsync();
            return x;
        }

        public async Task<bool> CheckNameExist(string name)
        {
            using var context=_factory.CreateDbContext();
            var obj = await context.Tags.FirstOrDefaultAsync(t => (t.TagName.ToLower()).Equals(name.ToLower()));
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
    }
}
