using ExpenseTracker.Application.DTOs.ExpenseCategory;
using FluentValidation;

namespace ExpenseTracker.Application.Validators
{
    public class ExpenseCategoryCreateDtoValidator : AbstractValidator<ExpenseCategoryCreateDto>
    {
        public ExpenseCategoryCreateDtoValidator()
        {
            RuleFor(e=>e.Name).NotEmpty().MaximumLength(30);
        }
    }
}
