using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
    public class ColorConverter
    {
        //convert an object from dal to entity
        public static ColorEntity toEntity(Colors c)
        {
            return new ColorEntity { colorId = c.ColorId, colorName = c.ColorName };
        }
        //convert an object from entity to dal
        public static Colors toDal(ColorEntity c)
        {
            return new Colors { ColorId = c.colorId, ColorName = c.colorName };

        }

        //convert a list of objects from dal to entity
        public static List<ColorEntity> toListEntity(List<Colors> lc)
        {
            List<ColorEntity> lce = new List<ColorEntity>();
            foreach (var item in lc)
            {
                lce.Add(toEntity(item));
            }
            return lce;
        }

        //convert a list of objects from entity to dal

        public static List<Colors> toListDal(List<ColorEntity> lc)
        {

            List<Colors> lce = new List<Colors>();
            foreach (var item in lc)
            {
                lce.Add(toDal(item));
            }
            return lce;
        }
    }
}
