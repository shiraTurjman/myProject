using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
   public interface ITagItemBLL
    {
        //Add TagItem 
        Task<int> AddTagItemAsync(TagItemEntity newTagItem);

        //get all Tag by item id שליפת כל האוטפיטים שפריט שייך אליהם
        Task<List<TagEntity>> GetAllByItemIdAsync(int itemId);

        //get Tag by TagId שליפת אוטפיט שלם 
        Task<List<ItemEntity>> GetAllByTagIdAsync(int TagId);

        //delete a TagItem  by TagItem id 
        Task<TagItemEntity> DeleteTagItemByTagItemIdAsync(int TagItemId);

        //delete Tag מחיקת אוטפיט שלם... כל הפריטים ששיכים אליו

        Task<List<TagItemEntity>> DeleteByTagIdAsync(int TagId);


        //delete TagItem by ItemId  מחיקת כל האוטפיטים שפריט מסויים שייך אליהם 
        Task<List<TagItemEntity>> DeleteByItemIdAsync(int itemId);

    }
}
