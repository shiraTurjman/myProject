using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;
using Dto;


namespace Bll.Functions
{
    public class ItemService : IItemService
    {

        private readonly IItemsRepository _IItemRepository;
        private readonly ITagItemRepository _TagItemRepository;
        private readonly ITagsRepository _TagRepository;
        private readonly IImageRepository _ImageRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        public ItemService(IItemsRepository IItemRepository, ITagItemRepository tagItemRepository,ITagsRepository tagRepository, IImageRepository imageRepository, ICategoriesRepository categoriesRepository)
        {
            _IItemRepository = IItemRepository;
            _TagItemRepository = tagItemRepository;
            _TagRepository = tagRepository;
            _ImageRepository = imageRepository;
            _categoriesRepository = categoriesRepository;

        }

        public async Task<int> AddItemAsync(AddItemDto item)
        {
            // return await _IItemRepository.AddItemAsync(item);
            var itemToAdd = new ItemEntity
            {
                CategoryId = item.CategoryId,
                ColorId = item.ColorId,
                UserId = item.UserId,
                Img = item.ImgId,
                EntryDate=item.EntryDate

            };

            var id=  await _IItemRepository.AddItemAsync(itemToAdd);
            //foreach(var tag in tagsId)
            //{
            //    await _TagItemRepository.AddTagItemAsync(new TagItemEntity() { ItemId = id, TagId = tag });
            //}
            return id;
        }

        public async Task DeleteItemAsync(int itemId)
        {
            await _IItemRepository.DeleteItemAsync(itemId);
        }

        public async Task<List<ItemDto>> GetByCategoryAsync(int cateroryId, int userId)
        {
            List<ItemDto> result = new List<ItemDto>();
            var itemsTemp= await _IItemRepository.GetByCategoryAsync(cateroryId, userId);
            foreach (var item in itemsTemp)
            {
                result.Add(
                    new ItemDto()
                    {
                        ItemId = item.ItemId,
                        CategoryId = item.CategoryId,
                        ColorId = item.ColorId,
                        UserId = item.UserId,
                        // ImgId=item.Img,
                        ImgData = await _ImageRepository.GetImageById(item.Img),
                        EntryDate = item.EntryDate
                    }); ;
            }
            return result;
        }

        public async Task<List<ItemDto>> GetByColorAsync(int colorId, int userId)
        {
            List <ItemDto> result = new List<ItemDto>();
            var itemsTemp= await _IItemRepository.GetByColorAsync(colorId, userId);
            foreach (var item in itemsTemp)
            {
                result.Add(
                    new ItemDto()
                    {
                        ItemId = item.ItemId,
                        CategoryId = item.CategoryId,
                        ColorId = item.ColorId,
                        UserId = item.UserId,
                        //ImgId = item.Img
                        ImgData = await _ImageRepository.GetImageById(item.Img)
                    });
            }
            return result;
        }

        public async Task<List<ItemDto>> GetByUserAsync(int userId)
        {
            List<ItemDto> result = new List<ItemDto>();
            var itemsTemp = await _IItemRepository.GetByUserAsync(userId);
            foreach (var item in itemsTemp)
            {
                result.Add(
                    new ItemDto()
                    {
                        ItemId = item.ItemId,
                        CategoryId = item.CategoryId,
                        ColorId = item.ColorId,
                        UserId = item.UserId,
                        ImgData = await _ImageRepository.GetImageById(item.Img)
                       // ImgId = item.Img
                    });
            }
            return result;

        }

        public async Task UpdateItemAsync(ItemEntity item)
        {
             await _IItemRepository.UpdateItemAsync(item);
                 
        }
    }
}
