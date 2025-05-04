using ExpenseTracker.Application.DTOs.Role;
using ExpenseTracker.Application.Interfaces;

namespace ExpenseTracker.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleService _roleService;

        public RoleService(IUnitOfWork unitOfWork, IRoleService roleService)
        {
            _unitOfWork = unitOfWork;
            _roleService = roleService;
        }

        public Task<List<RoleResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoleResponseDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<RoleResponseDto> CreateAsync(RoleCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync(int id, RoleUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
