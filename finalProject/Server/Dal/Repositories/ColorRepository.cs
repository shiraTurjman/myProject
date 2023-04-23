using Dal.Entities;
using Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Functions
{
    public class ColorRepository:IColorsRepository
    {
        private readonly IDbContextFactory<ServerDBContext> _factory;

        public ColorRepository(IDbContextFactory<ServerDBContext> factory)
        {
           _factory = factory;
        }

        //get list of colors from the database
        public async Task<List<ColorEntity>> getAllAsync()
        {
            using var context=_factory.CreateDbContext();   
            List<ColorEntity> result = new List<ColorEntity>();
            result=context.Colors.ToList();
            return result;
        }
    }
}
