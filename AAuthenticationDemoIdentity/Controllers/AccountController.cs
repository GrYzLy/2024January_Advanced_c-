using AAuthenticationDemoIdentity.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace AAuthenticationDemoIdentity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration) : Controller
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddOrUpdateAppUserModel model)
        {
            if (ModelState.IsValid)
            {
                var existedUser = await userManager.FindByNameAsync(model.UserName);
                if (existedUser != null)
                {
                    ModelState.AddModelError("", "User name is already taken");
                    return BadRequest(ModelState);
                }

                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePicture = model.ProfilePicture,
                };

                var result = await userManager.CreateAsync(user, model.Password);
                var roleResult = await userManager.AddToRoleAsync(user, model.Role);



                if (result.Succeeded && roleResult.Succeeded)
                {

                    var token = GenerateToken(user, model.UserName);
                    return Ok(new { token });
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var token = GenerateToken(user, model.UserName);
                        return Ok(new { token });
                    }

                    
                }


            }
            ModelState.AddModelError("", "Invalid username or password");
            return BadRequest(ModelState);
        }

            private async Task<string?> GenerateToken(AppUser user, string userName)
        {
            var secret = configuration["JwtConfig:Secret"];
            var issuer = configuration["JwtConfig:ValidIssuer"];
            var audience = configuration["JwtConfig:ValidAudiences"];

            if (secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the configuration!");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var tokenHandler = new JwtSecurityTokenHandler();

            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName)
            };
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptior);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

    }
}
    
