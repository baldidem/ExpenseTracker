using AutoMapper;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ExpenseCategoryCreateDto, ExpenseCategory>();
            CreateMap<ExpenseCategoryUpdateDto, ExpenseCategory>();
            CreateMap<ExpenseCategory, ExpenseCategoryResponseDto>();
        }
    }
}
