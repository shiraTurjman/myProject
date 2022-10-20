using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dal.Interfaces;
using Entities.Entities;

namespace Dal.Functions
{
    public class TagFunctions : ITags
    {
        public Task<int> AddTagByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<TagEntity> DeleteTagByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<TagEntity>> GetAllByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateTagByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
