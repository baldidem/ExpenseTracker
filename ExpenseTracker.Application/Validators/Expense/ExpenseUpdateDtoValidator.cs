using ExpenseTracker.Application.DTOs.Expense;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.Expense
{
    public class ExpenseUpdateDtoValidator : AbstractValidator<ExpenseUpdateDto>
    {
        public ExpenseUpdateDtoValidator()
        {
            RuleFor(x=>x.Amount).GreaterThanOrEqualTo(30);
        }
    }
}
