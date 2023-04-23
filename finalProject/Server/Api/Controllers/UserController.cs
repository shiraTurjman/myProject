using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bll.Interfaces;
using Dal.Entities;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("AddUser")]
        public  async Task<ActionResult<int>> AddUserAsync([FromBody] UserEntity newUser)
        {
            try
            {
                await _userService.AddUserAsync(newUser);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        [HttpGet("GetUser/{userId}")]
        public async Task< ActionResult<int>> GetUserByIdAsync(int userId)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(userId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<int>> UpdateUserByIdAsync([FromBody] UserEntity newUser)
        {
            try
            {
                await _userService.UpdateUserByIdAsync(newUser);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<int>> DeleteUserByIdAsync( int userId)
        {
            try
            {
                await _userService.DeleteUserByIdAsync(userId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserEntity>> LoginUserAsync([FromBody] LoginDto loginDto)
        {
            try
            {
               return await _userService.LoginAsync(loginDto);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex);
            }
        }






    }
}
