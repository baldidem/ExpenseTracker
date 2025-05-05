using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.Expense;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Application.Services.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdmin([FromQuery] int? userId)
        {
            var result = await _expenseService.GetAllForAdmin(userId);
            return Ok(new ApiResponse<List<ExpenseResponseDto>>(result));
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllForCurrentUser()
        {
            var result = await _expenseService.GetAllForCurrentUser();
            return Ok(new ApiResponse<List<ExpenseResponseDto>>(result));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int expenseId)
        {
            var result = await _expenseService.GetByIdAsync(expenseId);
            return Ok(new ApiResponse<ExpenseResponseDto>(result));
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Create([FromForm]ExpenseCreateDto dto)
        {
            var result = await _expenseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Update(int id, [FromForm] ExpenseUpdateDto dto)
        {
            var result = await _expenseService.UpdateAsync(id, dto);
            return Ok(new ApiResponse());
        }
        [HttpDelete]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int expenseId)
        {
            var result = await _expenseService.DeleteAsync(expenseId);
            return Ok(new ApiResponse());
        }
        [HttpPut("ExpenseStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateExpenseStatus(int id, [FromBody] ExpenseStatusDto dto)
        {
            var result = await _expenseService.UpdateExpenseStatus(id, dto);
            return Ok(new ApiResponse());
        }
    }
}
