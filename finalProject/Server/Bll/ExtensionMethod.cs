using Bll.Functions;
using Bll.Interfaces;
using Bll.Services;
using Dal.Entities;
using Dal.Functions;
using Dal.Interfaces;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Bll
{
    public static class ExtensionMethod
    {
        public static void InitDI(IServiceCollection service, string connectionString)
        {
            service.AddDbContextFactory<ServerDBContext>(item => item.UseSqlServer(connectionString));
            //dependency injection- check if all should be scoped or singelton
            //move to extension method
            service.AddScoped<ICategoriesRepository, CategoryRepository>();
            service.AddScoped<IColorsRepository, ColorRepository>();
            service.AddScoped<IEventRepository, EventRepository>();
            service.AddScoped<IItemsRepository, ItemRepository>();
            service.AddScoped<IOutfitItemRepository, OutfitItemRepository>();
            service.AddScoped<ITagsRepository, TagRepository>();
            service.AddScoped<IOutfitsRepository, OutfitsRepository>();
            service.AddScoped<ITagItemRepository, TagItemRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IImageRepository, ImageRepository>();


            // bll 
            service.AddScoped<ICategoriesService, CategoriesService>();
            service.AddScoped<IColorsService, ColorsService>();
            service.AddScoped<IEventService, EventService>();
            service.AddScoped<IItemService, ItemService>();
            service.AddScoped<IOutfitItemService, OutfitItemService>();
            service.AddScoped<IOutfitService, OutfitService>();
            service.AddScoped<ITagItemService, TagItemService>();
            service.AddScoped<ITagsService, TagsService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IImageService, ImageService>();


        }
    }
}
