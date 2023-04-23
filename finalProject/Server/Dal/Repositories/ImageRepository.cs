using Dal.Entities;
using Dal.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public class ImageRepository:IImageRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;
        public ImageRepository(IDbContextFactory<ServerDBContext>  factory)
        {
            _factory = factory;
        }

        public async Task<int> AddImage(IFormFile imageData)
        {
            using var context = _factory.CreateDbContext();
            try
            {
                var ImageDetails = new ImageDetails()
                {
                    ID = 0,
                    FileName = imageData.FileName,
                };

                using (var stream = new MemoryStream())
                {
                    imageData.CopyTo(stream);
                    ImageDetails.FileData = stream.ToArray();
                }

                var result = context.ImageDetails.Add(ImageDetails);
                await context.SaveChangesAsync();
                context.ImageDetails.Entry(ImageDetails).GetDatabaseValues();
                int id = ImageDetails.ID;
                return id;

            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<byte[]> GetImageById(int id)
        {
            using var context= _factory.CreateDbContext();
            var result = await context.ImageDetails.FirstOrDefaultAsync(i => i.ID == id);
            if(result != null)
            {
                return result.FileData;
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
