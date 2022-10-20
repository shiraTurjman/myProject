using System;
using System.Collections.Generic;
using System.Text;
using Entities.Entities;
using Dal.Models;

namespace Dal.Converters
{
    public class UserConverter
    {

        //convert an object from dal to entity
        public static UserEntity toEntity(Users u)
        {
            return new UserEntity {userId=u.UserId,userName=u.UserName, password=u.Password, address=u.Address, email=u.Email };
        }
        //convert an object from entity to dal
        public static Users toDal(UserEntity u)
        {
            return new Users { UserId=u.userId, UserName=u.userName, Password=u.password, Address=u.address, Email=u.email};

        }

        //convert a list of objects from dal to entity
        public static List<UserEntity> toListEntity(List<Users> lu)
        {
            List<UserEntity> lue = new List<UserEntity>();
            foreach (var item in lu)
            {
                lue.Add(toEntity(item));
            }
            return lue;
        }

        //convert a list of objects from entity to dal

        public static List<Users> toListDal(List<UserEntity> lue)
        {

            List<Users> lu = new List<Users>();
            foreach (var item in lue)
            {
                lu.Add(toDal(item));
            }
            return lu;
        }
    }
}
