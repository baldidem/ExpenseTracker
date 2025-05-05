using ExpenseTracker.Application.DTOs.Role;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.Role
{
    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
             .MaximumLength(10)
             .WithMessage("Role can be maximum 50 character!");
        }
    }
}
