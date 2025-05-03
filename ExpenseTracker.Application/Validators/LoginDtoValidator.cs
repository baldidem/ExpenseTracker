using ExpenseTracker.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .WithMessage("Email address is required in a correct format");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(30);
        }
    }
}
