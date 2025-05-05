using AutoMapper;
using ExpenseTracker.Application.DTOs.Role;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Mapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleCreateDto, Role>();
            CreateMap<Role,RoleResponseDto>();
        }
    }
}
