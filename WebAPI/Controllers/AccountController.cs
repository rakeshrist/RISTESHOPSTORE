using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{


    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;
        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.uow = uow;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReqDto)
        {
            var user = await uow.userRepository.Authenticate(loginReqDto.UserName, loginReqDto.Password);
            if (user == null)
                return Unauthorized();

            var loginRes = new LoginResDto();
            loginRes.UserName = user.user_name;
            loginRes.Token = CreateJWT(user);

            return Ok(loginRes);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (await uow.userRepository.UserAlreadyExists(loginReq.UserName))
            {
                return BadRequest("User already exist, please try another one");
            }
            uow.userRepository.Register(loginReq.UserName, loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        private string CreateJWT(tbl_user user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(secretKey));

            var claims = new Claim[]{
                 new Claim(ClaimTypes.Name,user.user_name),
                 new Claim(ClaimTypes.NameIdentifier,user.id.ToString())
             };

            var signingCredentials = new SigningCredentials(key,
            SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}