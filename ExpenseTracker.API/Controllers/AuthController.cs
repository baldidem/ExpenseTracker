using ExpenseTracker.Application.DTOs.Auth;
using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x=>x.Email == loginDto.Email);
            if (User == null)
            {
                return BadRequest(new ApiResponse("Email or password is incorrect!"));
            }
            //BURDA PASSWORD HASHLEYECEK METHOD CAGIRIP SIFREYI OYLE KONTROL EDECEGIM.
            return Ok();
        }
    }
}
