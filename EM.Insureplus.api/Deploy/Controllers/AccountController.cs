using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using EM.InsurePlus.Api.Models;
using EM.InsurePlus.Common.Constants;
using EM.InsurePlus.Services.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CO = EM.InsurePlus.Api.Models;
using SO = EM.InsurePlus.Services.Models;

namespace EM.InsurePlus.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger logger;        
        private readonly IUserService userService;
        private readonly IConfiguration configuration;       
        private readonly IMapper mapper;

        public AccountController(ILogger<AccountController> logger, 
            IUserService userService, 
            IMapper mapper, IConfiguration configuration)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> Token([FromBody] LoginModel model)
        {
            if (model == null
                || string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Invalid Input");
            }

            var isValid = await userService.SignInAsync(model.Username, model.Password);
            if (isValid)
            {
                var token = GenerateJwt(model.Username);

                return Ok(new
                {
                    token = token                    
                });
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<bool> Register([FromBody] UserModel model)
        {
            var mappedModel = mapper.Map<UserModel, SO.UserModel>(model);
            var result = await this.userService.Register(mappedModel, false);
            return result;
        }      

        private string GenerateJwt(string email)
        {

            var claims = new[]
            {
                          new Claim(JwtRegisteredClaimNames.Sub,email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtToken:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              configuration["JwtToken:Issuer"],
              configuration["JwtToken:Audience"],
              claims,
              expires: DateTime.UtcNow.AddHours(SystemConstants.JwtLifetime),
              signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}