using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using NextJsWebAPI.Models;
using NextJsWebAPI.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace NextJsWebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("api/signin")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]AccountModel model)
        {
            try
            {
                if (model == null)
                    model = new AccountModel();

                var user = new User();
                
                if(model.UserName == null && model.Email != null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                }
                if (model.UserName != null && model.Email == null)
                {
                    user = await _userManager.FindByNameAsync(model.UserName);
                }

                if (user == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                if (user.Role != 0) //user role = 0
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        id = user.Id,
                        role = user.Role,
                        token = GenerateJwtToken(model)
                    });
                }
            }
            catch (Exception e)
            {

            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [Route("api/signup", Name = "GetUser")] //, Name = "GetUser"
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AccountModel model) //add async Task<Result>
        {
            var userStore = _mapper.Map<User>(model);
            var manager = await _userManager.CreateAsync(userStore, model.Password);
            var user = new User
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,                
                Role = model.Role
            };

            if (manager.Succeeded)
            {
                return CreatedAtRoute("GetUser", new { id = userStore.Id }, user);
            }
            return BadRequest(manager.Errors);
        }

        private static string GenerateJwtToken(AccountModel model)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim (ClaimTypes.Name, model.UserName ?? model.Email)
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signinCredentials,
                Audience = "http://localhost:5000",
                Issuer = "http://localhost:5000"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
