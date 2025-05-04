using ExpenseTracker.Application.DTOs.Role;

namespace ExpenseTracker.Application.Services.Role
{
    public interface IRoleService
    {
        Task<List<RoleResponseDto>> GetAllAsync();
        Task<RoleResponseDto> GetByIdAsync(int id);
        Task<RoleResponseDto> CreateAsync(RoleCreateDto dto);
        Task<bool> UpdateAsync(int id, RoleUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
