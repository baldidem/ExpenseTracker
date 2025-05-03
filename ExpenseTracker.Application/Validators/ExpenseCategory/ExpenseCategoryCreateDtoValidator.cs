using ExpenseTracker.Application.DTOs.ExpenseCategory;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.ExpenseCategory
{
    public class ExpenseCategoryCreateDtoValidator : AbstractValidator<ExpenseCategoryCreateDto>
    {
        public ExpenseCategoryCreateDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(30).WithMessage("The expense category can be a maximum of 30 characters.");

        }
    }
}
