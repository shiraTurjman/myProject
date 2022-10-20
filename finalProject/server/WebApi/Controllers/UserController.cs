using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBLL bll;
        public UserController(IUserBLL _bll)
        {
            bll = _bll;
        }

        [HttpPost("AddUser")]
        public ActionResult<int> AddUser([FromBody] UserEntity newUser)
        {
            return Ok(bll.AddUserAsync(newUser));
        }

        [HttpGet("GetUser/{userId}")]
        public ActionResult<int> GetUserById(int userId)
        {
            return Ok(bll.GetUserByIdAsync(userId));
        }

        [HttpPut("UpdateUser")]
        public ActionResult<int> UpdateUserById([FromBody] UserEntity newUser)
        {
            return Ok(bll.UpdateUserByIdAsync(newUser));
        }
        [HttpDelete("DeleteUser/{userId}")]
        public ActionResult<int> DeleteUserById( int userId)
        {
            return Ok(bll.DeleteUserByIdAsync(userId));
        }






    }
}
