using AutoMapper;
using ExpenseTracker.Application.DTOs.ExpenseCategory;
using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Mapper
{
    public class ExpenseCategoryMapper : Profile
    {
        public ExpenseCategoryMapper()
        {
            CreateMap<ExpenseCategoryCreateDto, ExpenseCategory>();
            //CreateMap<ExpenseCategoryUpdateDto, ExpenseCategory>();
            CreateMap<ExpenseCategory, ExpenseCategoryResponseDto>();
        }
    }
}
