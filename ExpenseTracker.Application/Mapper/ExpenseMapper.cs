using AutoMapper;
using ExpenseTracker.Application.DTOs.Expense;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;

namespace ExpenseTracker.Application.Mapper
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            CreateMap<ExpenseCreateDto, Expense>();
            CreateMap<Expense, ExpenseResponseDto>()
           .ForMember(dest => dest.CategoryName, opt =>
               opt.MapFrom(src => src.ExpenseCategory != null ? src.ExpenseCategory.Name : string.Empty))
           .ForMember(dest => dest.UserFullName, opt =>
               opt.MapFrom(src => src.User != null ? src.User.Name + " " + src.User.Surname : string.Empty))
           .ForMember(dest => dest.ExpenseStatus, opt =>
               opt.MapFrom(src => src.ExpenseStatus.ToString()));
        }
    }
}
