
using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;

namespace Bll.Functions
{
    public class OutfitService : IOutfitService
    {
        private readonly IOutfitsRepository _IOutfitsRepository;
        public OutfitService(IOutfitsRepository IOutfitsRepository)
        {
            _IOutfitsRepository = IOutfitsRepository;
        }


        public async Task<int> AddOutfitAsync(OutfitEntity c)
        {
            return await _IOutfitsRepository.AddOutfitAsync(c);
        }

        public async Task DeleteOutfitAsync(int outfitId)
        {
             await _IOutfitsRepository.DeleteOutfitAsync(outfitId);
        }

        public async Task<List<OutfitEntity>> GetByUserIdAsync(int userId)
        {
            return await _IOutfitsRepository.GetByUserIdAsync(userId);
        }

        public async Task<int> UpdateOutfitAsync(OutfitEntity outfit)
        {
            return await _IOutfitsRepository.UpdateOutfitAsync(outfit);
        }
    }
}
