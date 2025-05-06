using ExpenseTracker.Application.DTOs.User;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MaximumLength(50).WithMessage("Name must be at most 50 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required.")
                .MaximumLength(50).WithMessage("Surname must be at most 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("RoleId must be greater than 0.");

            RuleFor(x => x.Iban)
                .NotEmpty().WithMessage("IBAN is required.")
                .Must(iban => iban.StartsWith("TR") && iban.Length == 26)
                .WithMessage("IBAN must start with 'TR' and be 26 characters long.");
        }
    }
}
