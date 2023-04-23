using Dal.Entities;

namespace Bll.Interfaces
{
   public interface ITagItemService
    {
        //Add TagItem 
        Task AddTagItemAsync(TagItemEntity newTagItem);

        //get all Tag by item id שליפת כל האוטפיטים שפריט שייך אליהם
        Task<List<TagEntity>> GetAllByItemIdAsync(int itemId);

        //get Tag by TagId שליפת אוטפיט שלם 
        Task<List<ItemEntity>> GetAllByTagIdAsync(int TagId);

        //delete a TagItem  by TagItem id 
        Task DeleteTagItemByTagItemIdAsync(int TagItemId);

        //delete Tag מחיקת אוטפיט שלם... כל הפריטים ששיכים אליו

        Task DeleteByTagIdAsync(int TagId);


        //delete TagItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        Task DeleteByItemIdAsync(int itemId);

    }
}
