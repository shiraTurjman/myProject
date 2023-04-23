using Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dal.Entities;

namespace Api.Controllers
{
    public class OutfitItemController : ControllerBase
    {
        private readonly IOutfitItemService _outfitItemService;
        public OutfitItemController(IOutfitItemService outfitItemService)
        {
            _outfitItemService = outfitItemService;
        }

        //Add outfit item
        [HttpPost("AddOutfitItem")]
        public async Task<ActionResult<int>> AddOutfitItem([FromBody] OutfitItemEntity newOutfitItem)
        {
            try
            {
                return Ok(await _outfitItemService.AddOutfitItemAsync(newOutfitItem));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllByItemId/{userId}")]
        public async Task<ActionResult<int>> GetAllByItemId(int itemId)
        {
            try
            {
                return Ok( await _outfitItemService.GetAllByItemIdAsync(itemId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllByOutfitId/{outfitId}")]
        public async Task<ActionResult<int>> GetAllByOutfitId(int outfitId)
        {
            try
            {
                return Ok( await _outfitItemService.GetAllByOutfitIdAsync(outfitId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteByOutfitItemId/{outfitItemId}")]
        public async Task<ActionResult<int>> DeleteOutfitItemByOutfitItemId(int outfitItemId)
        {
            try
            {
                await _outfitItemService.DeleteOutfitItemByOutfitItemIdAsync(outfitItemId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("DeleteByOutfitId/{outfitId}")]
        public async Task<ActionResult<int>> DeleteByOutfitIdAsync(int outfitId)
        {
            try
            {
                await _outfitItemService.DeleteByOutfitIdAsync(outfitId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpDelete("DeleteByItemId/{itemId}")]
        public async Task<ActionResult<int>> DeleteByItemIdAsync(int itemId)
        {
            try
            {
                await _outfitItemService.DeleteByItemIdAsync(itemId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

