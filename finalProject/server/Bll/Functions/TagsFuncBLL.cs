using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Interfaces;
using Entities.Entities;

namespace Bll.Functions
{
    public class TagsFuncBLL : ITagsBLL
    {
        ITags dal;
        public TagsFuncBLL(ITags _dal)
        {
            dal = _dal;
        }
        public async Task<int> AddTagByUserIdAsync(int userId)
        {
            return await dal.AddTagByUserIdAsync(userId);
        }

        public async Task<TagEntity> DeleteTagByUserIdAsync(int userId)
        {
            return await dal.DeleteTagByUserIdAsync(userId);
        }

        public async Task<List<TagEntity>> GetAllByUserIdAsync(int userId)
        {
            return await dal.GetAllByUserIdAsync(userId);
        }

        public async Task<int> UpdateTagByUserIdAsync(int UserId)
        {
            return await dal.UpdateTagByUserIdAsync(UserId);
        }
    }
}
