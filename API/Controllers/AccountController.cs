using API.Data;
using API.Entities;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService) {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        // parameters are expected to be received as part of an object
        public async Task<ActionResult<AccountUserDTO>> Register(RegisterDTO registerDTO) {
            using var hmac = new HMACSHA512();
            if(await UserExists(registerDTO.Username)) return BadRequest("Username is taken");
            var user = new AppUser {
                Username = registerDTO.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new AccountUserDTO() {
                Username = registerDTO.Username,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountUserDTO>> Login(LoginDTO loginDTO) {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDTO.Username);
            if(user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for(int i = 0; i < computedHash.Length; i++) {
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new AccountUserDTO {
                Username = user.Username, 
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username) {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}