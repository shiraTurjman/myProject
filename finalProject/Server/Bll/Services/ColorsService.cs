using Bll.Interfaces;
using Dal.Entities;
using Dal.Interfaces;

namespace Bll.Functions
{
    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository _IColorsRepository;
        public ColorsService(IColorsRepository IColorsRepository)
        {
            _IColorsRepository= IColorsRepository;
        }

        public async Task<List<ColorEntity>> getAllAsync()
        {
            return await _IColorsRepository.getAllAsync();
        }
    }
}
