using ExpenseTracker.Application.DTOs.Expense;

namespace ExpenseTracker.Application.Services.Expense
{
    public interface IExpenseService
    {
        Task<List<ExpenseResponseDto>> GetAll(int? userId = null); //Bu admin icin tum expenseleri listeleyen method.
        Task<List<ExpenseResponseDto>> GetAllForCurrentUser(); // Bu personel icin.
        Task<List<ExpenseResponseDto>> GetAllByParameter(ExpenseFilterDto filter);
        Task<ExpenseResponseDto> CreateAsync(ExpenseCreateDto dto);
        Task<bool> Update(int id, ExpenseUpdateDto dto);
        Task<bool> Delete(int id);
    }
}
