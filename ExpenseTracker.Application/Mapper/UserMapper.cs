using AutoMapper;
using ExpenseTracker.Application.DTOs.User;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : src.RoleName));
        }
    }
}
