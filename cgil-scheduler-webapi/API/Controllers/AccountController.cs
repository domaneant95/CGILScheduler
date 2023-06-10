using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using Domain;
using API.Services;
using Application.Core;
using Domain.Dto;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public UserManager<AppUser> UserManager { get; }
        public TokenService TokenService { get; }
        public CryptographyService CryptographyService { get; }

        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, CryptographyService cryptographyService)
        {
            UserManager = userManager;
            TokenService = tokenService;
            CryptographyService = cryptographyService;
        }

        [HttpGet("public/key")]
        public async Task<ActionResult<string>> Login()
        {
            return CryptographyService.RSAParameters.PublicKey.ToString();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            loginDto.Password = CryptographyService.GetRSA().Decrypt(loginDto.Password);
            var user = await UserManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();
            var result = await UserManager.CheckPasswordAsync(user, loginDto.Password);
            
            if (result)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = TokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Username is already taken");
            }

            if (await UserManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email is already taken");
            }

            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await UserManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Image = null,
                    Token = TokenService.CreateToken(user),
                    Username = user.UserName
                };
            }


            return BadRequest(result.Errors);
        }

        /* DA RICONTROLLARE
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }

        int iterCount = (int)ReadNetworkByteOrder(Convert.FromBase64String(user.PasswordHash), 5);
        int saltLength = (int)ReadNetworkByteOrder(Convert.FromBase64String(user.PasswordHash), 9);
            // Read the salt: must be >= 128 bits
            if (saltLength< 128 / 8)
                {
                    await Console.Out.WriteLineAsync("false");
        }
        byte[] salt = new byte[saltLength];
        Buffer.BlockCopy(Convert.FromBase64String(user.PasswordHash), 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = Convert.FromBase64String(user.PasswordHash).Length - 13 - salt.Length;
                if (subkeyLength< 128 / 8)
                {
                    await Console.Out.WriteLineAsync("false");
    }
        byte[] expectedSubkey = new byte[subkeyLength];
        Buffer.BlockCopy(Convert.FromBase64String(user.PasswordHash), 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);
        KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(Convert.FromBase64String(user.PasswordHash), 1);
        byte[] actualSubkey = KeyDerivation.Pbkdf2("Pa$$w0rd", salt, prf, iterCount, subkeyLength);
        */
    }
}
