using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Task_Manager.Models;
using Task_Manager.Models.DTO;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthActions : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthActions(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registration([FromBody] UserRegistration requestDTO)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(requestDTO.Email!);
                if (userExists is not null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>(){
                            "This Email Is Already Exists"
                        },
                        Result = false
                    });
                }

                var newUser = new IdentityUser()
                {
                    UserName = requestDTO.Name,
                    Email = requestDTO.Email,
                };

                var isCreated = await _userManager.CreateAsync(newUser, requestDTO.Password!);
                if (isCreated.Succeeded)
                {
                    if (await _roleManager.RoleExistsAsync("user"))
                    {
                        var AddRoleResult = await _userManager.AddToRoleAsync(newUser, "user");
                        if (AddRoleResult.Succeeded)
                        {
                            var token = await GenerateWebToken(newUser);
                            return Ok(new AuthResult() { Token = token, Result = true });
                        }
                        var errorMessages = AddRoleResult.Errors.Select(e => e.Description).ToList();
                        return BadRequest(new AuthResult() { Result = false, Errors = errorMessages });
                    }
                    else
                    {
                        var role = new IdentityRole()
                        {
                            Name = "user",
                        };
                        await _roleManager.CreateAsync(role);
                        var AddRoleResult = await _userManager.AddToRoleAsync(newUser, "user");
                        if (AddRoleResult.Succeeded)
                        {
                            var token = await GenerateWebToken(newUser);
                            return Ok(new AuthResult() { Token = token, Result = true });
                        }
                        var errorMessages = AddRoleResult.Errors.Select(e => e.Description).ToList();
                        return BadRequest(new AuthResult() { Result = false, Errors = errorMessages });
                    }
                }
                else
                {
                    var errorMessages = isCreated.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new AuthResult() { Result = false, Errors = errorMessages });
                }


            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var existUser = await _userManag    
                if (existUser is null) return BadRequest(new AuthResult() { Result = false, Errors = new List<string>() { "Invalid Credentials, This Email user doesnot exist" } });
                var isPasswordCorrect = await _userManager.CheckPasswordAsync(existUser, requestDTO.Password!);
                if (!isPasswordCorrect) return BadRequest(new AuthResult() { Result = false, Errors = new List<string>() { "Invalid Credentials, The Password is correct" } });
                var role = await _userManager.GetRolesAsync(existUser);
                var token = await GenerateWebToken(existUser);
                return Ok(new AuthResult() { Result = true, Token = token, });

            }
            return BadRequest(new AuthResult()
            {
                Result = false,
                Errors = new List<string>() { "Invalid Data" }
            });
        }

        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole([FromBody] RoleDTO requestDTO)
        {
            if (ModelState.IsValid)
            {
                var UserExists = await _userManager.FindByEmailAsync(requestDTO.Email!);
                if (UserExists is null) return BadRequest(new AuthResult() { Result = false, Errors = new List<string>() { "This User Does not Exist" } });
                if (await _roleManager.RoleExistsAsync(requestDTO.RoleName!))
                {
                    var result = await _userManager.AddToRoleAsync(UserExists, requestDTO.RoleName!);
                    if (!result.Succeeded)
                    {
                        var errorMessages = result.Errors.Select(e => e.Description).ToList();
                        return BadRequest(new AuthResult() { Result = false, Errors = errorMessages });
                    }
                    return Ok("Role Added Successfully");
                }
                return BadRequest(new AuthResult() { Result = false, Errors = new List<string>() { "This role doesnot exist" } });
            }

            return BadRequest(new AuthResult() { Result = false, Errors = new List<string>() { "Invalid Data" } });

        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("The Role Exists");
            }
            var IdentityRole = new IdentityRole(roleName);
            var Result = await _roleManager.CreateAsync(IdentityRole);
            if (!Result.Succeeded)
            {
                var errorMessages = Result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new AuthResult() { Result = false, Errors = errorMessages });
            }
            return Created("/Roles", IdentityRole);
        }


        private async Task<string> GenerateWebToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value!));
            var role = await _userManager.GetRolesAsync(user);
            var roles = string.Join(", ", role);
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new("Role", roles),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),

           };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);



        }
    }
}