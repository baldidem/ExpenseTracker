using ExpenseTracker.Application.DTOs.ExpenseCategory;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.ExpenseCategory
{
    public class ExpenseCategoryUpdateDtoValidator : AbstractValidator<ExpenseCategoryUpdateDto>
    {
        public ExpenseCategoryUpdateDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(30).WithMessage("The expense category can be a maximum of 30 characters.");

        }
    }
}
