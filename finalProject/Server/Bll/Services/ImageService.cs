

using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Bll.Services
{
    public class ImageService:IImageService
    {
        private readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<int> AddImage(IFormFile img)
        {
          return await  _imageRepository.AddImage(img); 
        }
        public async Task<byte[]> GetImageById(int id)
        {
           return await _imageRepository.GetImageById(id);
        }

    }
}
