using AutoMapper;
using ExpenseTracker.Application.DTOs.User;
using ExpenseTracker.Application.Interfaces;
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

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<List<UserResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
