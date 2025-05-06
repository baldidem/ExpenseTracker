using ExpenseTracker.Application.DTOs.User;
using FluentValidation;

namespace ExpenseTracker.Application.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
           .MaximumLength(50).WithMessage("Name must be at most 50 characters.")
           .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Surname)
                .MaximumLength(50).WithMessage("Surname must be at most 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Surname));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is not valid.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.RoleId)
                .GreaterThan(0).WithMessage("RoleId must be greater than 0.")
                .When(x => x.RoleId.HasValue);

            RuleFor(x => x.Iban)
                .Must(iban => iban.StartsWith("TR") && iban.Length == 26)
                .WithMessage("IBAN must start with 'TR' and be 26 characters long.")
                .When(x => !string.IsNullOrEmpty(x.Iban));
        }
    }
}
