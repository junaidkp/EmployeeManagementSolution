using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Authentication APIs (Login, Token generation )
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Login user and generate JWT token
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT Token</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == request.Username);

            if (user == null)
                return Unauthorized(ApiResponse<string>.ErrorResponse("Invalid credentials"));

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(ApiResponse<string>.ErrorResponse("Invalid credentials"));

            var token = _jwtService.GenerateToken(user);

            return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
        }
    }
}