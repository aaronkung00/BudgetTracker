using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost()]
        public async Task<IActionResult> AddUser(AddUserRequestModel addUserRequest)
        {
            if (await _userService.IsExist(addUserRequest.Email))
                return BadRequest("Email has been already registered!");

            var response =  await _userService.AddUser(addUserRequest);

            return Ok(response);
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateUser(AddUserRequestModel addUserRequest)
        {
            await _userService.UpdateUser(addUserRequest);
            return Ok("Update Successed!");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return Ok("Delete Successed!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

        
            return Ok(user);
        }

    }
}
