using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace Bll.Interfaces
{
    public interface IColorsBLL
    {
        //get list of all colors 
        Task<List<ColorEntity>> getAllAsync();
    }
}
