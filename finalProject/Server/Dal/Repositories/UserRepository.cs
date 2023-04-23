using Microsoft.EntityFrameworkCore;
using Dal.Interfaces;
using Dal.Entities;

namespace Dal.Functions
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public UserRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }
        public async Task AddUserAsync(UserEntity user)
        {
            try
            {
                using var context = _factory.CreateDbContext();
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            UserEntity userToDelete = await context.Users.FindAsync(userId);
            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Could not delete user");
            }

        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var user = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("Could not find user");
            }
            return user;
        }

        public async Task<int> UpdateUserByIdAsync(UserEntity user)
        {
            using var context = _factory.CreateDbContext();
            var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (userToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 

                userToUpdate.UserName = user.UserName;
                userToUpdate.Password = user.Password;
                userToUpdate.Email = user.Email;
                return await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Could not find user to update.");
            }

        }
        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            using var context = _factory.CreateDbContext();
            return await context.Users.AnyAsync(c => c.Email.Equals(email));
        }

        public async Task<bool> CheckPasswordValidAsync(string email, string password)
        {
            using var context = _factory.CreateDbContext();
            var user = await context.Users.FirstAsync(c => c.Email.Equals(email));
            return user.Password.Equals(password);

        }
        public async Task<UserEntity> getUserByEmailAsync(string email)
        {
            using var context = _factory.CreateDbContext();
            var user = await context.Users.FirstAsync(u => u.Email.Equals(email));
            if (user != null)
            {
                return user;
            }
            else
                throw new Exception("Could not find user with given email.");
        }
    }
}
