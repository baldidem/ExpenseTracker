using AutoMapper;
using ExpenseTracker.Application.DTOs.Role;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllByParametersAsync(x => x.IsActive);
            var mappedList = _mapper.Map<List<RoleResponseDto>>(roles);
            return mappedList;
        }

        public async Task<RoleResponseDto> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User Id must be greater than zero.");
            }
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            var mapped = _mapper.Map<RoleResponseDto>(role);
            return mapped;
        }
        public async Task<RoleResponseDto> CreateAsync(RoleCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }

            var role = _mapper.Map<Domain.Entities.Role>(dto);
            await _unitOfWork.RoleRepository.CreateAsync(role);
            await _unitOfWork.SaveChangesAsync();
            var mapped = _mapper.Map<RoleResponseDto>(_mapper);
            return mapped;
        }
        public async Task<bool> UpdateAsync(int id, RoleUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Role Id must be greater than zero.");
            }
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id {id} was not found.");
            }
            role.Name = dto.Name;
            _unitOfWork.RoleRepository.Update(role);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Role Id must be greater than zero.");
            }
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id {id} was not found.");
            }
            _unitOfWork.RoleRepository.Delete(role);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
