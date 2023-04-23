using Microsoft.AspNetCore.Mvc;
using Bll.Interfaces;
using Dal.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
          
        private readonly IColorsService _colorsService;
        public ColorsController(IColorsService colorsService)
        {
            _colorsService = colorsService;   
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ColorEntity>>> getAll()
        {
            try
            {
                return Ok( await _colorsService.getAllAsync());
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
