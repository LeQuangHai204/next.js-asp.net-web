using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Api.Models;

namespace Api.Controllers
{
    [Route("api/v0/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IJwtService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            foreach (string role in registerDto.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return BadRequest($"Role '{role}' does not exist.");
                }
            }

            AppUser user = new()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            IdentityResult createUserResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (!createUserResult.Succeeded)
                return StatusCode(500, "Error creating user: "
                    + string.Join(", ", createUserResult.Errors.Select(error => error.Description)));

            foreach (string role in registerDto.Roles)
            {
                IdentityResult assignRoleResult = await _userManager.AddToRoleAsync(user, role);
                if (!assignRoleResult.Succeeded)
                    return StatusCode(500, "Error assigning role to user: "
                        + string.Join(", ", assignRoleResult.Errors.Select(error => error.Description)));
            }

            // If user creation and role assignment are successful, return a successful response
            return Created(null as Uri, new { JwtToken = new JwtTokenDto { Payload = _jwtService.GenerateToken(user) } });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null) return NotFound("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded) return Ok(new { JwtToken = new JwtTokenDto { Payload = _jwtService.GenerateToken(user) } });
            // if (result.Succeeded)
            // {
            //     string jwtToken = _jwtService.GenerateToken(user);
            //     CookieOptions cookieOptions = new()
            //     {
            //         HttpOnly = true,
            //         Secure = true, // Use true in production
            //         SameSite = SameSiteMode.Strict, // Adjust as necessary
            //         Expires = DateTime.UtcNow.AddHours(1)
            //     };

            //     HttpContext.Response.Cookies.Append("JwtToken", jwtToken, cookieOptions);
            //     return NoContent();
            // }

            return Unauthorized("Invalid username or password");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully");
        }
    }
}