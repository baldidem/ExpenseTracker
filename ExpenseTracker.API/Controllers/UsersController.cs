using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.Role;
using ExpenseTracker.Application.DTOs.User;
using ExpenseTracker.Application.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(new ApiResponse<List<UserResponseDto>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(new ApiResponse<UserResponseDto>(result));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto model)
        {
            var result = await _userService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto model)
        {
            var result = await _userService.UpdateAsync(id, model);
            return Ok(new ApiResponse());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok(new ApiResponse());
        }
    }
}
