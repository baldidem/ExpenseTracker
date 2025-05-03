using ExpenseTracker.Application.DTOs.Auth;
using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Application.Settings;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBcryptPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IUnitOfWork unitOfWork, IBcryptPasswordHasher passwordHasher, ITokenService tokenService, JwtSettings jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null || !user.IsActive)
            {
                return BadRequest(new ApiResponse("Email or password is incorrect!"));
            }

            var passwordConfirmed = _passwordHasher.ConfirmPassword(loginDto.Password, user.PasswordHash);
            if (!passwordConfirmed)
            {
                return BadRequest(new ApiResponse("Email or password is incorrect!"));
            }

            var token = _tokenService.GenerateToken(user);

            var response = new TokenResponseDto
            {
                UserId = user.Id,
                Token = token,
                Email = user.Email,
                ExpireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes)
            };
            return Ok(new ApiResponse<TokenResponseDto>(response));
        }
    }
}
