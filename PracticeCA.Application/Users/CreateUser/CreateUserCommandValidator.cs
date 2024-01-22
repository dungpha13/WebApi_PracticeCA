using FluentValidation;

namespace PracticeCA.Application;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        ConfigureValidationRules();
    }

    private void ConfigureValidationRules()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must is 6 length");
    }
}


