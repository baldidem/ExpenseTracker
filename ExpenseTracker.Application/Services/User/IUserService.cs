using ExpenseTracker.Application.DTOs.User;

namespace ExpenseTracker.Application.Services.User
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetByIdAsync(int id);
        Task<UserResponseDto> CreateAsync(UserCreateDto dto);
        Task<bool> UpdateAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
