using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Converters;
using Dal.Interfaces;
using Dal.Models;
using Entities.Entities;

namespace Dal.Functions
{
    public class UserFunctions : IUser
    {
        FinalProjectContext db;
        public UserFunctions(FinalProjectContext _db)
        {
            db = _db;
        }
        public async Task<int> AddUserAsync(UserEntity user)
        {
            db.Users.Add(UserConverter.toDal(user));
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<int> DeleteUserByIdAsync(int userId)
        {
            Users userToDelete = db.Users.Find(userId);
            db.Users.Remove(userToDelete);
            int res = await db.SaveChangesAsync();
            return res;
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            var user =  db.Users.FirstOrDefault(u => u.UserId == userId);
            return UserConverter.toEntity(user);
        }

        public async Task<int> UpdateUserByIdAsync(UserEntity user)
        {
            var userToUpdate = db.Users.FirstOrDefault(u => u.UserId == user.userId);
            if (userToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 

                userToUpdate.UserName = user.userName;
                userToUpdate.Password = user.password;
                userToUpdate.Address = user.address;
                userToUpdate.Email = user.email;
            }
            int res = await db.SaveChangesAsync();
            return res;
        }
    }
}
