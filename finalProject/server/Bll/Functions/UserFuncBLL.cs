using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Interfaces;
using Entities.Entities;

namespace Bll.Functions
{
    public class UserFuncBLL : IUserBLL
    {
        IUser dal;
        public UserFuncBLL(IUser _dal)
        {
            dal = _dal;
        }
        public async Task<int> AddUserAsync(UserEntity user)
        {
            return await dal.AddUserAsync(user);
        }

        public async Task<int> DeleteUserByIdAsync(int userId)
        {
            return await dal.DeleteUserByIdAsync(userId);
        }

        public  async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            return await dal.GetUserByIdAsync(userId);
        }

        public async Task<int> UpdateUserByIdAsync(UserEntity user)
        {
            return await dal.UpdateUserByIdAsync(user);
        }
    }
}
