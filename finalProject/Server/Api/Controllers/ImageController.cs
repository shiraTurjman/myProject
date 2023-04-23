using Bll.Interfaces;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService=imageService;
        }
        //Add item 

        [HttpPost("AddImage")]
        public async Task<ActionResult<int>> AddImage([FromForm] IFormFile file)
        {
            string name = file.FileName;
            string extension = Path.GetExtension(file.FileName);
            //read the file
            //using (var memoryStream = new MemoryStream())
            //{
            //    file.CopyTo(memoryStream);
            //}
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
               return  await _imageService.AddImage(file);
            }
            catch (Exception)
            {
                throw;
            }
                
        }

        [HttpGet("GetImageById/{id}")]
        public async Task<ActionResult<byte[]>> GetImageById(int id)
        {
            try
            {
                var imageData = await _imageService.GetImageById(id);
                return imageData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);    
            }

        }

    }
    }

