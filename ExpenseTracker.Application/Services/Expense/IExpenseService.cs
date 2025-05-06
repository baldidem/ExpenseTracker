using ExpenseTracker.Application.DTOs.Expense;

namespace ExpenseTracker.Application.Services.Expense
{
    public interface IExpenseService
    {
        Task<List<ExpenseResponseDto>> GetAllForAdmin(int? userId = null); //Bu admin icin tum expenseleri listeleyen method.
        Task<List<ExpenseResponseDto>> GetAllForCurrentUser(); // Bu personel icin.
        //Task<List<ExpenseResponseDto>> GetByParametersForCurrentUser(ExpenseFilterDto filter);
        Task<ExpenseResponseDto> GetByIdAsync(int expenseId);
        Task<ExpenseResponseDto> CreateAsync(ExpenseCreateDto dto);
        Task<bool> UpdateAsync(int id, ExpenseUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateExpenseStatus(int expenseId,ExpenseStatusDto dto);
    }
}
