using Bll.Interfaces;
using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagItemController : ControllerBase
    {
        private readonly ITagItemService _tagItemService;
        public TagItemController(ITagItemService tagItemService)
        {
            _tagItemService = tagItemService;
        }

        [HttpPost("AddTagItem")]
        public async Task<ActionResult<int>> AddTagItemAsync([FromBody] TagItemEntity newTagItem)
        {
            try
            {
                await _tagItemService.AddTagItemAsync(newTagItem);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllByItemId/{itemId}")]
        public async Task<ActionResult<int>> GetAllByItemId(int itemId)
        {
            try
            {
                return Ok(await _tagItemService.GetAllByItemIdAsync(itemId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllByTagId/{tagId}")]
        public async Task<ActionResult<int>> GetAllByTagId(int tagId)
        {
            try
            {
                return Ok(await _tagItemService.GetAllByTagIdAsync(tagId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("DeleteTagItemByTagItemIdAsync/{tagItemId}")]
        public async Task<ActionResult<int>> DeleteTagItemByTagItemId(int tagItemId)
        {
            try
            {
                await _tagItemService.DeleteTagItemByTagItemIdAsync(tagItemId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete TagItem By TagItemId
        [HttpDelete("DeleteByTagIdAsync/{tagId}")]
        public async Task<ActionResult<int>> DeleteByTagIdAsync(int tagId)
        {
            try
            {
                await _tagItemService.DeleteByTagIdAsync(tagId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete TagItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        [HttpDelete("DeleteByItemIdAsync/{itemId}")]
        public async Task<ActionResult<int>> DeleteByItemId(int itemId)
        {
            try
            {
                await _tagItemService.DeleteByItemIdAsync(itemId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
