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
    public class ItemsController : ControllerBase
    {
        IItemBLL bll;
        public ItemsController(IItemBLL _bll)
        {
            bll = _bll;
        }

        //Add item 

        [HttpPost("AddItem")]
        public ActionResult<int> AddItem([FromBody]ItemEntity newItem)
        {
            return Ok(bll.AddItemAsync(newItem));
        }

        //delete item 

        

        [HttpDelete("DeleteItem/{itemId}")]
        public ActionResult<int> DeleteItem(int itemId)
        {
            return Ok(bll.DeleteItemAsync(itemId));
        }

        //update item

        [HttpPut("UpdateItem")]
        public ActionResult<int> UpdateItem([FromBody]ItemEntity item)
        {
            return Ok(bll.UpdateItemAsync(item));
        }
        //get item dy user

        [HttpGet("GetByUser/{userId}")]
        public ActionResult<int> GetByUser(int userId)
        {
            return Ok(bll.GetByUserAsync(userId));
        }
        //get item by color

        [HttpGet("GetByColor/{colorId}/{userId}")]
        public ActionResult<int> GetByColor(int colorId,int userId)
        {
            return Ok(bll.GetByColorAsync(colorId,userId));
        }

        //get item by category

        [HttpGet("GetByCategory/{cateroryId}/{userId}")]
        public ActionResult<int> GetByCategory(int cateroryId,int userId)
        {
            return Ok(bll.GetByCategoryAsync(cateroryId,userId));
        }

    }
}
