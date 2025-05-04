using ExpenseTracker.Application.DTOs.Expense;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Validators.Expense
{
    public class ExpenseCreateDtoValidator : AbstractValidator<ExpenseCreateDto>
    {
        public ExpenseCreateDtoValidator()
        {
            RuleFor(x=>x.Amount).NotEmpty();
            RuleFor(x=>x.ExpenseCategoryId).NotEmpty();

        }
    }
}
