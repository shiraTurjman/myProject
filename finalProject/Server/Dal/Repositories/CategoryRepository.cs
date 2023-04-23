using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class CategoryRepository : ICategoriesRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public CategoryRepository(IDbContextFactory<ServerDBContext> factory)
        {
            _factory = factory;
        }
        //add category
        public async Task AddCategoryAsync(CategoryEntity category)
        {
            using var context = _factory.CreateDbContext();
            await context.AddAsync(category);
            await context.SaveChangesAsync();

        }

        //delete category returns the object
        public async Task<CategoryEntity> DeleteCategoryAsync(int categoryId)
        {
            using var context = _factory.CreateDbContext();
            var category = context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            if (category != null)
            {
                context.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
            else
            {
                throw new Exception("Couldn't delete.");
            }
        }

        //get list by user id
        public async Task<List<CategoryEntity>> GetByUserIdAsync(int userId)
        {
            using var context = _factory.CreateDbContext();
            var list = await context.Categories.Where(c => c.UserId == userId).ToListAsync();
            if (list.Count > 0)
                return list;
            else
                throw new Exception("No categories exist for the given id");

        }

        //update 
        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            using var context = _factory.CreateDbContext();
            var categoryToUpdate = context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (categoryToUpdate != null)
            {//לעשות המרה לבד כדי לא לאבד מצביע 
                categoryToUpdate.CategoryId = category.CategoryId;
                categoryToUpdate.CategoryName = category.CategoryName;
                categoryToUpdate.UserId = category.UserId;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The category you are trying to update doesn't exist.");
            } 
        }
        public async Task<bool> CheckNameExist(string name)
        {
            using var context = _factory.CreateDbContext();
            var obj = await context.Categories.FirstOrDefaultAsync(c => (c.CategoryName.ToLower()).Equals(name.ToLower()));
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
