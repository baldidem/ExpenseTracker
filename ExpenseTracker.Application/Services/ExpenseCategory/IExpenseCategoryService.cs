using ExpenseTracker.Application.DTOs.Common;
using ExpenseTracker.Application.DTOs.ExpenseCategory;

namespace ExpenseTracker.Application.Services.ExpenseCategory
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategoryResponseDto>> GetAllAsync();
        Task<ExpenseCategoryResponseDto?> GetByIdAsync(int id);
        Task<ExpenseCategoryResponseDto> CreateAsync(ExpenseCategoryCreateDto dto);
        Task<bool> Update(int id, ExpenseCategoryUpdateDto dto);
        Task<bool> Delete(int id);

    }
}
