using ExpenseTracker.Application.DTOs.Expense;

namespace ExpenseTracker.Application.Services.Expense
{
    public interface IExpenseService
    {
        Task<List<ExpenseResponseDto>> GetAllForAdmin(int? userId = null);
        Task<List<ExpenseResponseDto>> GetAllForCurrentUser();
        Task<ExpenseResponseDto> GetByIdAsync(int expenseId);
        Task<ExpenseResponseDto> CreateAsync(ExpenseCreateDto dto);
        Task<bool> UpdateAsync(int id, ExpenseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateExpenseStatus(int expenseId,ExpenseStatusDto dto);
    }
}
