using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
   public interface ITagItem
    {
        //Add TagItem 
        Task<int> AddTagItemAsync(TagItemEntity newTagItem);

        //get all tag by item id
        Task<List<TagItemEntity>> GetAllByItemIdAsync(int itemId);

        //get item by tag
        Task<List<TagItemEntity>> GetAllByOutfitIdAsync(int tagId);

        //delete a tagitem  by tagItem id 
        Task<TagItemEntity> DeleteTagItemByTagItemIdAsync(int tagItemId);

        //delete tag מחיקת תגית שלם... כל הפריטים ששיכים אליו

        Task<List<TagItemEntity>> DeleteByTagIdAsync(int tagId);


        //delete tagitem by ItemId  מחיקת כל התגיות שפריט מסויים שייך אליהם 
        Task<List<TagItemEntity>> DeleteByItemIdAsync(int itemId);

    }
}
