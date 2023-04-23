
using Dal.Entities;
using Dto;

namespace Bll.Interfaces
{
    public interface IUserService
    {
        //add user
        Task AddUserAsync(UserEntity user);

        //delete user by id
        Task DeleteUserByIdAsync(int userId);

        //update user by user id 
        Task<int> UpdateUserByIdAsync(UserEntity user);

        //get user by id 
        Task<UserEntity> GetUserByIdAsync(int userId);

        Task<UserEntity> LoginAsync(LoginDto loginDto);
    }
}
