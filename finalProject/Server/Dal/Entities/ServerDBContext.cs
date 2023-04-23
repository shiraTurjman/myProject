using Microsoft.EntityFrameworkCore;

namespace Dal.Entities
{
    public class ServerDBContext:DbContext
    {
        public  DbSet<CategoryEntity> Categories { get; set; }
        public  DbSet<ColorEntity> Colors { get; set; }
        public  DbSet<EventEntity> Events { get; set; }
        public  DbSet<ItemEntity> Items { get; set; }
        public  DbSet<OutfitItemEntity> OutfitItems { get; set; }
        public  DbSet<OutfitEntity> Outfits { get; set; }
        public  DbSet<TagItemEntity> TagItems { get; set; }
        public  DbSet<TagEntity> Tags { get; set; }
        public  DbSet<UserEntity> Users { get; set; }
        public  DbSet<UseEntity> Uses { get; set; }
        public  DbSet<ImageDetails> ImageDetails { get; set; }

        public ServerDBContext(DbContextOptions<ServerDBContext> options):base(options)
        {

        }
        public ServerDBContext()
        {

        }
    }
}
