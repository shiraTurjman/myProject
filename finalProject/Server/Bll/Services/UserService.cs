
using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;
using Dto;

namespace Bll.Functions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository IUserRepository)
        {
            _userRepository = IUserRepository;
        }
        public async Task AddUserAsync(UserEntity user)
        {
            try
            {
                await _userRepository.AddUserAsync(user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
             await _userRepository.DeleteUserByIdAsync(userId);
        }

        public  async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<int> UpdateUserByIdAsync(UserEntity user)
        {
            return await _userRepository.UpdateUserByIdAsync(user);
        }

        public async Task<UserEntity> LoginAsync( LoginDto loginDto)
        {
            if(await _userRepository.CheckEmailExistsAsync(loginDto.Email) && await _userRepository.CheckPasswordValidAsync(loginDto.Email, loginDto.Password))
            {
                return await _userRepository.getUserByEmailAsync(loginDto.Email);
            }
            else
            {
                throw new Exception("Email or password not valid.");
            }
        }
    }
}
