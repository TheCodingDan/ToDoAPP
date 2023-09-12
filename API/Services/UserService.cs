using System.Security.Cryptography;
using System.Text;
using API.Controllers;
using API.Data;
using API.Domain.DTO;
using API.Domain.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public UserService(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<ActionResult<UserDTO>> RegisterUser(RegisterDTO registerDTO){
            
            if(await UserExists(registerDTO.Username)){
                return Unauthorized("Username already taken!");
            } else{

                using var hmac = new HMACSHA512();


                var user = new User{
                    Username = registerDTO.Username.ToLower(),
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                    PasswordSalt = hmac.Key
                };

                _context.Users.Add(user);

                await _context.SaveChangesAsync();

                return new UserDTO{
                    Username = user.Username,
                    Token = _tokenService.CreateToken(user)
                };
            }

            
        }

        public async Task<ActionResult<UserDTO>> LoginUser(LoginDTO loginDTO){
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginDTO.Username.ToLower());

            if(user == null){
                return Unauthorized("Invalid Username!");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDTO{
                Username = user.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username){
            return await _context.Users.AnyAsync(User => User.Username == username);
        }

    }
}