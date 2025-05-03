using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Application.Services.ExpenseCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IExpenseCategoryService _expenseCategoryService;
        public ExpenseCategoriesController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var expenseCategories = await _expenseCategoryService.GetAllAsync();

            return Ok(new ApiResponse<List<ExpenseCategoryResponseDto>>(expenseCategories));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var expenseCategory = await _expenseCategoryService.GetByIdAsync(id);

            return Ok(new ApiResponse<ExpenseCategoryResponseDto>(expenseCategory));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseCategoryCreateDto model)
        {
            var result = await _expenseCategoryService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExpenseCategoryUpdateDto model)
        {
            var result = await _expenseCategoryService.Update(id, model);
            return Ok(new ApiResponse());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _expenseCategoryService.Delete(id);
            return Ok(new ApiResponse());
        }
    }
}
