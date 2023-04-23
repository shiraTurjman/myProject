using Dal.Entities;
using Microsoft.AspNetCore.Http;


namespace Dal.Interfaces
{
    public interface IImageRepository
    {
       Task<int> AddImage(IFormFile imageData);
        Task<byte[]> GetImageById(int id);
    }
}
