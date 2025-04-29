using AutoMapper;
using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        // Expense Category baska bir tabloda tutulacak. Bu yonetici rolu icin yetkilendirilecek.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpenseCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() // Bu tum expense categoryleri listeleyen method.
        {
            var expenseCategories = await _unitOfWork.GetRepository<ExpenseCategory>().GetByParametersAsync(x=>x.IsActive);
            if (!expenseCategories.Any())
            {
                return NotFound(new ApiResponse("No registered expense category found in the system!"));
            }
            var mappedList = _mapper.Map<List<ExpenseCategory>>(expenseCategories);
            return Ok(new ApiResponse<List<ExpenseCategory>>(mappedList));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExpenseCategoryCreateDto model)
        {
            var mapped = _mapper.Map<ExpenseCategory>(model);
            mapped.CreatedDate = DateTime.UtcNow;
            //mapped.CreatedUserId = ?????
            await _unitOfWork.GetRepository<ExpenseCategory>().CreateAsync(mapped);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
}
