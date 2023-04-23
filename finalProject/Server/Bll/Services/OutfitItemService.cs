
// GETלא כתוב ממישוש הפונקציות 

using Bll.Interfaces;
using Dal.Interfaces;
using Dal.Entities;

namespace Bll.Functions
{
    public class OutfitItemService : IOutfitItemService
    {
        private readonly IOutfitItemRepository _IOutfitItemRepository;
        
        public OutfitItemService(IOutfitItemRepository IOutfitItemRepository)
        {
            _IOutfitItemRepository = IOutfitItemRepository; 
        }

        public async Task<int> AddOutfitItemAsync(OutfitItemEntity newOutfitItem)
        {
           return await _IOutfitItemRepository.AddOutfitItemAsync(newOutfitItem);
        }

        public async Task DeleteByItemIdAsync(int itemId)
        {
            await _IOutfitItemRepository.DeleteByItemIdAsync(itemId);
        }

        public async Task DeleteByOutfitIdAsync(int outfitId)
        {
           await _IOutfitItemRepository.DeleteByOutfitIdAsync(outfitId);
        }

        public async Task DeleteOutfitItemByOutfitItemIdAsync(int outfitItemId)
        {
           await _IOutfitItemRepository.DeleteOutfitItemByOutfitItemIdAsync(outfitItemId);
        }

     
        //need to finish these two functions
        public async Task<List<OutfitEntity>> GetAllByItemIdAsync(int itemId)
        {
            //return  await _IOutfitItemRepository.GetAllByItemIdAsync(itemId);
           throw new NotImplementedException();
        }

        //לסיים..... איך לגשת לפונקציה ממחלקה אחרת .....
        //public async Task<List<OutfitEntity>> GetAllByItemIdAsync(int itemId)
        //{
        //    var list = await dal.GetAllByItemIdAsync(itemId);
        //    List<OutfitEntity> outfitList = new List<OutfitEntity>();
        //    foreach(OutfitItemEntity item in list)
        //    {
        //      //  outfitList.Add()
        //    }

        //}

        public async Task<List<ItemEntity>> GetAllByOutfitIdAsync(int outfitId)
        {
            //  return await _IOutfitItemRepository.GetAllByOutfitIdAsync(outfitId);
            throw new NotImplementedException();
        }
    }
}
