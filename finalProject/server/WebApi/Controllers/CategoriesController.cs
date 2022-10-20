// האם עושים את הפונקציות אסינכרוניות ??? 
//במה במקבל מהנתב כותבים רק שם כמו : category ??? 


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Entities.Entities;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoriesBLL bll;
        public CategoriesController(ICategoriesBLL _bll)
        {
            bll = _bll;
        }
        //get by user id
        [HttpGet("GetByUserId/{id}")]
        public ActionResult<List<CategoryEntity>> getByUserId(int id)
        {
            return Ok(bll.GetByUserIdAsync(id));
        }
        //update category 
        [HttpPut("UpdateCategory")]
        public ActionResult<int> UpdateCategory([FromBody]CategoryEntity category)
        {
            return Ok(bll.UpdateCategoryAsync(category));
        }
        //delete
        [HttpDelete("DeleteCategory/{id}")]
        public ActionResult<CategoryEntity> DeleteCategory(int id)
        {
            return Ok(bll.DeleteCategoryAsync(id));
        }

        [HttpPost("AddCategory")]
        public ActionResult<int> AddCategory([FromBody] CategoryEntity category)
        {
            return Ok(bll.AddCategoryAsync(category));
        }






    }
}

    