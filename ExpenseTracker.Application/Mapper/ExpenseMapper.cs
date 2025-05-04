using AutoMapper;
using ExpenseTracker.Application.DTOs.Expense;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Mapper
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            CreateMap<ExpenseCreateDto, Expense>();
            CreateMap<Expense, ExpenseResponseDto>()
           .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ExpenseCategory.Name))
           .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.Name + " " + src.User.Surname))
           .ForMember(dest => dest.ExpenseStatus, opt => opt.MapFrom(src => src.ExpenseStatus.ToString()));
        }
    }
}
