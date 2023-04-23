
using Dal.Entities;

namespace Dal.Interfaces
{
    public interface IUserRepository
    {
        //add user
        Task AddUserAsync(UserEntity user);

        //delete user by id
        Task DeleteUserByIdAsync(int userId);

        //update user by user id 
        Task<int> UpdateUserByIdAsync(UserEntity user);

        //get user by id 
        Task<UserEntity> GetUserByIdAsync(int userId);

        //functions for login 
        Task<UserEntity> getUserByEmailAsync(string email);
        Task<bool> CheckPasswordValidAsync(string email, string password);
        Task<bool> CheckEmailExistsAsync(string email);

    }
}
