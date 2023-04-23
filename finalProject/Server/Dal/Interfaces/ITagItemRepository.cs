using Dal.Entities;


namespace Dal.Interfaces
{
   public interface ITagItemRepository
    {
        //Add TagItem 
        Task<int> AddTagItemAsync(TagItemEntity newTagItem);

        //get all tag by item id
        Task<List<TagEntity>> GetAllByItemIdAsync(int itemId);
        Task<List<ItemEntity>> GetAllByTagIdAsync(int tagId);

        //get item by tag
        Task<List<TagItemEntity>> GetAllByOutfitIdAsync(int tagId);

        //delete a tagitem  by tagItem id 
        Task DeleteTagItemByTagItemIdAsync(int tagItemId);

        //delete tag מחיקת תגית שלם... כל הפריטים ששיכים אליו

        Task DeleteByTagIdAsync(int tagId);


        //delete tagitem by ItemId  מחיקת כל התגיות שפריט מסויים שייך אליהם 
        Task DeleteByItemIdAsync(int itemId);

    }
}
