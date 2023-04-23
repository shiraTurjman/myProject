using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;


namespace Bll.Functions
{
    public class CategoriesService : ICategoriesService
    {
         private readonly ICategoriesRepository _ICategoryRepository;
        public CategoriesService(ICategoriesRepository ICategoryRepository)
        {
           _ICategoryRepository= ICategoryRepository;
        }

        public async Task AddCategoryAsync(CategoryEntity c)
        {
             await _ICategoryRepository.AddCategoryAsync(c);
        }

        public async Task<CategoryEntity> DeleteCategoryAsync(int categoryId)
        {
            return await _ICategoryRepository.DeleteCategoryAsync(categoryId);
          //  return dal.DeleteCategoryAsync(categoryId);

        }

        public async Task<List<CategoryEntity>> GetByUserIdAsync(int userId)
        {
            return await _ICategoryRepository.GetByUserIdAsync(userId);

        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            await _ICategoryRepository.UpdateCategoryAsync(category);
        }
        public async Task<bool> CheckNameExist(string name)
        {
            return await _ICategoryRepository.CheckNameExist(name);
        }
    }
}
