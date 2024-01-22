using FluentValidation;

namespace PracticeCA.Application;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(v => v.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");
    }
}
