using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;
        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<ActionResult<int>> GetAllByUserId(int userId)
        {
            try
            {
                return Ok(await _tagsService.GetAllByUserIdAsync(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddTagAsync")]
        public async Task<ActionResult<int>> AddTag([FromBody] TagEntity newTag)
        {
            try
            {
                await _tagsService.AddTagAsync(newTag);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteTagByTagId/{tagId}")]
        public async Task<ActionResult<int>> DeleteTagByTagIdAsync(int tagId)
        {
            try
            {
                return Ok(await _tagsService.DeleteTagByTagIdAsync(tagId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateTag")]
        public async Task<ActionResult<int>> UpdateTag([FromBody] TagEntity tag)
        {
            try
            {
                return Ok(await _tagsService.UpdateTagAsync(tag));
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
                return await _tagsService.CheckNameExist(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
