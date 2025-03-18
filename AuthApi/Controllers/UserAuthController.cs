using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthApi.Data;
using AuthApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //baseUrl/api/UserAuth
    public class UserAuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly string? _jwtKey;
        private readonly string? _JwtIssuer;
        private readonly string? _JwtAudience;
        private readonly int _JwtExpiry;

        public UserAuthController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager,
            IConfiguration configuration)
            
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _jwtKey = configuration["Jwt:Key"];
            _JwtIssuer = configuration["Jwt:Issuer"];
            _JwtAudience = configuration["Jwt:Audience"];
            _JwtExpiry = int.Parse(configuration["Jwt:ExpiryMinutes"]);
        }
        //baseUrl/api/UserAuth/Register
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if(registerModel == null || string.IsNullOrEmpty(registerModel.Name) || string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.Password))
            {
                return BadRequest("Invalid Registration details");
            }
            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if(existingUser != null)
            {
                return Conflict("Email already Exists");
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                Name = registerModel.Name,
            };
           var result = await _userManager.CreateAsync(user,registerModel.Password);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User Created Successfully");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if(user == null)
            {
                return Unauthorized(new { success = false, message = "Invalid Usrename or Password" });
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginModel.Password,false);
            if(!result.Succeeded)
            {
                return Unauthorized(new { success = false, message = "Invalid Usrename or Password" });
            }
            var token = GenerateJWTToken(user);
            return Ok(new {success = true ,token}); 
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
           await _signinManager.SignOutAsync();
            return Ok("User Loggedout successfully");
        }
        private string GenerateJWTToken(ApplicationUser user)
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("Name",user.Name)
                 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_JwtIssuer,
                         _JwtAudience,
                         Claims,
                         expires: DateTime.Now.AddMinutes(_JwtExpiry),
                         signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        } 
    }
}
 