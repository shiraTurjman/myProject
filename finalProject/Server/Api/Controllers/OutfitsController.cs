using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Bll.Interfaces;
using Dal.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitsController : ControllerBase
    {
        private readonly IOutfitService _outfitService;
        public OutfitsController(IOutfitService outfitService)
        {
            _outfitService = outfitService;
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<ActionResult<int>> GetByUserId(int userId)
        {
            try
            {
                return Ok( await _outfitService.GetByUserIdAsync(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("AddOutfit")]
        public async Task<ActionResult<int>> AddOutfitAsync([FromBody] OutfitEntity newOutfit)
        {
            try
            {
                return Ok(await _outfitService.AddOutfitAsync(newOutfit));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //delete outfit by outfit id
        [HttpDelete("DeleteOutfit/{outfitId}")]
        public async Task<ActionResult<int>> DeleteOutfit(int outfitId)
        {
            try
            {
                await _outfitService.DeleteOutfitAsync(outfitId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("UpdateOutfit")]
        public async Task<ActionResult<int>> UpdateOutfit([FromBody] OutfitEntity outfit)
        {
            try
            {
                return Ok(await _outfitService.UpdateOutfitAsync(outfit));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
