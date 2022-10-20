using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Dal.Interfaces
{
     public interface IUser
    {
        //add user
        Task<int> AddUserAsync(UserEntity user);

        //delete user by id
        Task<int> DeleteUserByIdAsync(int userId);

        //update user by user id 
        Task<int> UpdateUserByIdAsync(UserEntity user);

        //get user by id 
        Task<UserEntity> GetUserByIdAsync(int userId);
    }
}
