using System;
using System.Collections.Generic;
using System.Text;
using Bll.Interfaces;
using Entities.Entities;
using Dal.Functions;
using Dal.Interfaces;
using System.Threading.Tasks;

namespace Bll.Functions
{
    public class ColorsFuncBLLcs : IColorsBLL
    {
        Icolors dal;
        public ColorsFuncBLLcs(Icolors _dal)
        {
            dal = _dal;
        }

        public async Task<List<ColorEntity>> getAllAsync()
        {
            return await dal.getallAsync();
        }
    }
}
