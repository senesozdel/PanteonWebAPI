using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PanteonWebAPI.Interfaces;
using PanteonWebAPI.Models.Entities;
using PanteonWebAPI.Models.ResponseModels;

namespace PanteonWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        public Task<IEnumerable<User>> GetAllUsers()
        {
            return _user.GetAllUsersAsync();
        }

        [HttpGet("{userId}")]
        public Task<User> GetUserById([FromQuery] int userId)
        {
            return _user.GetUserByIdAsync(userId);
        }

        [HttpPost("addUser")]

        public Task<AddedUserResponse> AddUser([FromBody] User user)
        {
            return _user.AddUserAsync(user);
        }

        [HttpDelete("deleteUser")]

        public Task DeleteUserAsync([FromQuery] int userId)
        {
            return _user.DeleteUserAsync(userId);
        }


        [HttpPut("editUser")]

        public Task<User> UpdateUser([FromBody] User user)
        {
            return _user.UpdateUserAsync(user);
        }
    }
}
