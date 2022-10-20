using System;
using System.Collections.Generic;
using System.Text;
using Bll.Interfaces;
using Entities.Entities;
using Dal.Interfaces;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class CategoriesBLL : ICategoriesBLL
    {
        Icategories dal;
        public CategoriesBLL(Icategories _dal)
        {
            dal = _dal;
        }

        public async Task<int> AddCategoryAsync(CategoryEntity c)
        {
          return await dal.AddCategoryAsync(c);
        }

        public async Task<CategoryEntity> DeleteCategoryAsync(int categoryId)
        {
            return await dal.DeleteCategoryAsync(categoryId);
          //  return dal.DeleteCategoryAsync(categoryId);

        }

        public async Task<List<CategoryEntity>> GetByUserIdAsync(int userId)
        {
            return await dal.GetByUserIdAsync(userId);

        }

        public async Task<int> UpdateCategoryAsync(CategoryEntity category)
        {
            return await dal.UpdateCategoryAsync(category);

        }
    }
}
