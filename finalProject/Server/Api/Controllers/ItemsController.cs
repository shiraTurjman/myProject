using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bll.Interfaces;
using Dal.Entities;
using Dto;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly  IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
           _itemService= itemService; 
        }

 //HEAD
        ////Add item 

        //[HttpPost("AddItem")]
        //public async Task<int> AddItem([FromBody]ItemEntity newItem, [FromBody] List<int> TagIds)
        //{
        //    try
        //    {
        //        return await _itemService.AddItemAsync(newItem,TagIds);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
           
        //}

        //Add item

        [HttpPost("AddItem")]
        public async Task<int> AddItem([FromBody] AddItemDto newItem)
        {
            //[FromBody] List<int> TagIds
            try
            {
                return await _itemService.AddItemAsync(newItem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
 //77d72b4e904b4d66f7b3ea351c7639f588b0d83c

       // delete item



        [HttpDelete("DeleteItem/{itemId}")]
        public async Task<ActionResult<int>> DeleteItem(int itemId)
        {
            try
            {
                await _itemService.DeleteItemAsync(itemId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        //update item

        [HttpPut("UpdateItem")]
        public async Task<ActionResult<int>> UpdateItem([FromBody]ItemEntity item)
        {
            try
            {
                await _itemService.UpdateItemAsync(item);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        //get item by user

        [HttpGet("GetByUser/{userId}")]
        public async Task<List<ItemDto>> GetByUser(int userId)
        {
            try
            {
                return await _itemService.GetByUserAsync(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        //get item by color

        [HttpGet("GetByColor/{colorId}/{userId}")]
        public async Task<List<ItemDto>> GetByColor(int colorId,int userId)
        {
            try
            {
                return await _itemService.GetByColorAsync(colorId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        //get item by category

        [HttpGet("GetByCategory/{cateroryId}/{userId}")]
        public async Task<List<ItemDto>> GetByCategory(int cateroryId,int userId)
        {
            try
            {
                return await _itemService.GetByCategoryAsync(cateroryId, userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }


        //[HttpPost("UploadImage")]
        //public async Task<ActionResult<int>> UploadImage([FromForm] ItemEntity newItem)
        //{
        //    try
        //    {
        //        return Ok(await _itemService.AddItemAsync(newItem));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}

    }
}
