using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Dal.Interfaces
{
    public interface Icolors
    {
        //get list of all colors 
         Task<List<ColorEntity>> getallAsync();
    }
}
