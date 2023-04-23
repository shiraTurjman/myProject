

using Dal.Entities;

namespace Bll.Interfaces
{
    public interface IColorsService
    {
        //get list of all colors 
        Task<List<ColorEntity>> getAllAsync();
    }
}
