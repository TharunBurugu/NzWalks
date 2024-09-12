using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.IRepository;
using NZWalksAPI.Models.DTO;
using System.ComponentModel;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public ITokenRepositoty TokenRepositoty { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepositoty tokenRepositoty)
        {
            this.userManager = userManager;
            TokenRepositoty = tokenRepositoty;
        }
        // post : /api/Auth/Register

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                // Add roles to this User 
                if (registerRequestDto.Roles != null)
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User successfully Register! Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Passwrod);

                if (checkPasswordResult)
                {
                    // Get the roles for user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = TokenRepositoty.CreateJwtToken(user, roles.ToList());
                        //  for a future purpose we can also return some other information.
                        // So let's create a class for this one as well.
                        //Something like a login response.
                        //DTO So solution Explorer Inside the models inside the folder I will create a new class

                        var response = new LoginResponceDto()
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password incorrect");
        }
    }
}
