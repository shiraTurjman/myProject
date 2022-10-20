using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Dal.Interfaces
{
     public interface ITags
    {
        //return all tags for current user by id
        Task<List<TagEntity>> GetAllByUserIdAsync(int userId);

        //add a tag by user id
        Task<int> AddTagByUserIdAsync(int userId);

        //delete a tag by user id 
        Task<TagEntity> DeleteTagByUserIdAsync(int userId);

        //update a tag bu userId
        Task<int> UpdateTagByUserIdAsync(int userId);


    }
}
