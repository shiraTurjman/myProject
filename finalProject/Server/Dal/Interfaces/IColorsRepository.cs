
using Dal.Entities;

namespace Dal.Interfaces
{
    public interface IColorsRepository
    {
        //get list of all colors 
         Task<List<ColorEntity>> getAllAsync();
    }
}
