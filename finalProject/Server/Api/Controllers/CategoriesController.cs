// האם עושים את הפונקציות אסינכרוניות ??? 
//במה במקבל מהנתב כותבים רק שם כמו : category ??? 

using Microsoft.AspNetCore.Mvc;
using Bll.Interfaces;
using Dal.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoryService;
        public CategoriesController(ICategoriesService categoryService)
        {
            _categoryService = categoryService;
        }
        //get by user id
        [HttpGet("GetByUserId/{id}")]
        public async Task <ActionResult<List<CategoryEntity>>> getByUserId(int id)
        {
            try
            {
                return Ok( await _categoryService.GetByUserIdAsync(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        //update category 
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult> UpdateCategory([FromBody] CategoryEntity category)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        //delete
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<CategoryEntity>> DeleteCategory(int id)
        {
            try
            {
                return Ok( await _categoryService.DeleteCategoryAsync(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<int>> AddCategory([FromBody] CategoryEntity category)
        {
            try
            {
                await _categoryService.AddCategoryAsync(category);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpGet("CheckNameExist")]
        public async Task<ActionResult<bool>> CheckNameExist(string name)
        {
            try
            {
                return await _categoryService.CheckNameExist(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }






    }
}

