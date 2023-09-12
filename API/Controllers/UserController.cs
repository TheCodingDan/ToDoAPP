using API.Domain.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> RegisterUser(RegisterDTO registerDTO){
            return await _userService.RegisterUser(registerDTO);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginUser(LoginDTO loginDTO){
            return await _userService.LoginUser(loginDTO);
        }

        
    }
}