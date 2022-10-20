using System;
using System.Collections.Generic;
using System.Text;
using Dal.Models;
using Dal.Interfaces;
using Dal.Converters;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dal.Functions
{
    public class ColorFunctions:Icolors
    {
        
        FinalProjectContext db;
        public ColorFunctions(FinalProjectContext _db)
        {
            db = _db;
        }
        //get list of colors from the database
        public async Task<List<ColorEntity>> getallAsync()
        {// take care 
            //db.Colors.
            return ColorConverter.toListEntity( await db.Colors.ToListAsync());
        }
    }
}
