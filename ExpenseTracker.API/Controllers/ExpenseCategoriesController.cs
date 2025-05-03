//using AutoMapper;
//using ExpenseTracker.Application.DTOs.Common;
//using ExpenseTracker.Application.DTOs.ExpenseCategory;
//using ExpenseTracker.Application.Interfaces;
//using ExpenseTracker.Application.Services;
//using ExpenseTracker.Application.Services.ExpenseCategory;
//using ExpenseTracker.Domain.Entities;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ExpenseTracker.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class ExpenseCategoriesController : ControllerBase
//    {
//        // Expense Category baska bir tabloda tutulacak. Bu yonetici rolu icin yetkilendirilecek.
//        //private readonly IUnitOfWork _unitOfWork;
//        //private readonly IMapper _mapper;
//        //private readonly IHttpContextAccessor _contextAccessor;

//        //IExpenseCategoriesService

//        //public ExpenseCategoriesController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
//        //{
//        //    _unitOfWork = unitOfWork;
//        //    _mapper = mapper;
//        //    _contextAccessor = contextAccessor;
//        //}
//        private readonly IExpenseCategoryService _expenseCategoryService;

//        public ExpenseCategoriesController(IExpenseCategoryService expenseCategoryService)
//        {
//            _expenseCategoryService = expenseCategoryService;
//        }

//        [HttpGet]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> GetAll() // Bu tum expense categoryleri listeleyen method.
//        {
//            var expenseCategories = await _expenseCategoryService.GetAll();
//            if (!expenseCategories.Any())
//            {
//                return NotFound(new ApiResponse("No registered expense category found in the system!"));
//            }

//            return Ok(new ApiResponse<List<ExpenseCategoryResponseDto>>(expenseCategories));
//        }
//        [HttpGet("id")]
//        public async Task<IActionResult> GetById([FromRoute] int id)
//        {
//            var expenseCategory = await _expenseCategoryService.GetById(id);
//            if (expenseCategory == null)
//            {
//                return NotFound(new ApiResponse("Expense Category is not found!"));
//            }

//            return Ok(new ApiResponse<ExpenseCategoryResponseDto>(expenseCategory));
//        }

//        [HttpPost]
//        [Authorize(Roles = "Admin")]
//        public async Task<IActionResult> Create([FromBody] ExpenseCategoryCreateDto model)
//        {
//            var result = await _expenseCategoryService.Create(model);
//            return Ok(new ApiResponse<int>(result));
//        }

//        //public async Task<IActionResult> Update([FromBody] ExpenseCategoryUpdateDto model)
//        //{


//        //}

//        //public async Task<IActionResult> Delete([FromRoute] int id)
//        //{
//        //    return Ok();
//        //}
//    }
//}
