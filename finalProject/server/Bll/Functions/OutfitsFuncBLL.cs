using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using Entities.Entities;
using Dal.Interfaces;

namespace Bll.Functions
{
    public class OutfitsFuncBLL : IOutfitsBLL
    {
        IOutfits dal;
        public OutfitsFuncBLL(IOutfits _dal)
        {
            dal = _dal;
        }

        
        public async Task<int> AddOutfitAsync(OutfitEntity c)
        {
            return await dal.AddOutfitAsync(c);
        }

        public async Task<OutfitEntity> DeleteoutfitAsync(int outfitId)
        {
            return await dal.DeleteoutfitAsync(outfitId);
        }

        public async Task<List<OutfitEntity>> GetByUserIdAsync(int userId)
        {
            return await dal.GetByUserIdAsync(userId);
        }

        public async Task<int> UpdateOutfitAsync(OutfitEntity outfit)
        {
            return await dal.UpdateOutfitAsync(outfit);
                }
    }
}
