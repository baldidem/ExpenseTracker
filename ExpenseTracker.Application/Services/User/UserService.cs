using AutoMapper;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Application.DTOs.User;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBcryptPasswordHasher _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IBcryptPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllByParametersAsync(x=>x.IsActive);
            var mappedList = _mapper.Map<List<UserResponseDto>>(users);
            return mappedList;
        }
        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            if (id<=0)
            {
                throw new ArgumentException("User Id must be greater than zero.");
            }
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} was not found.");
            }

            var mapped = _mapper.Map<UserResponseDto>(user);    
            return mapped;
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }
            var existingUser = await _unitOfWork.UserRepository
                .FirstOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same email address already exists.");
            }
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with id {dto.RoleId} was not found.");
            }

            var user = _mapper.Map<ExpenseTracker.Domain.Entities.User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
            user.RoleName = role.Name;

            await _unitOfWork.UserRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User id must be greater than zero.");
            }

            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Input data is required.");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} was not found.");
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && !dto.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExists = await _unitOfWork.UserRepository
                    .FirstOrDefaultAsync(x => x.Email.ToLower() == dto.Email.ToLower() && x.Id != id);

                if (emailExists != null)
                {
                    throw new InvalidOperationException("A user with the same email address already exists.");
                }

                user.Email = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.Iban) && !dto.Iban.Equals(user.Iban, StringComparison.OrdinalIgnoreCase))
            {
                var ibanExists = await _unitOfWork.UserRepository
                    .FirstOrDefaultAsync(x => x.Iban.ToLower() == dto.Iban.ToLower() && x.Id != id);

                if (ibanExists != null)
                {
                    throw new InvalidOperationException("A user with the same iban already exists.");
                }

                user.Email = dto.Email;
            }

            if (dto.RoleId.HasValue && dto.RoleId != user.RoleId)
            {
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(dto.RoleId.Value);
                if (role == null)
                {
                    throw new KeyNotFoundException($"Role with id {dto.RoleId.Value} was not found.");
                }

                user.RoleId = dto.RoleId.Value;
                user.RoleName = role.Name;
            }
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(dto.Password);
            }

            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                user.Name = dto.Name;
            }

            if (!string.IsNullOrWhiteSpace(dto.Surname))
            {
                user.Surname = dto.Surname;
            }

            if (!string.IsNullOrWhiteSpace(dto.Iban))
            {
                user.Iban = dto.Iban;
            }

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("User id must be greater than zero.");
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, "Expenses");
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id {id} was not found.");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException("User is already deleted.");
            }

            // 🔥 Eğer kullanıcının aktif expenses'leri varsa silinemez kuralı koymak istersen:
            if (user.Expenses != null && user.Expenses.Any(e => e.IsActive))
            {
                throw new InvalidOperationException("User has active expenses and cannot be deleted.");
            }

            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
