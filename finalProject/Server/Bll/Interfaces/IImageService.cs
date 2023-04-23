

using Dal.Entities;
using Microsoft.AspNetCore.Http;

namespace Bll.Interfaces
{
    public interface IImageService
    {
        Task<int> AddImage(IFormFile img);
        Task<byte[]> GetImageById(int id);
    }
}
