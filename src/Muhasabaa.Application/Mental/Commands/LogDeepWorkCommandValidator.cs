using FluentValidation;

namespace Muhasabaa.Application.Mental.Commands;

public class LogDeepWorkCommandValidator : AbstractValidator<LogDeepWorkCommand>
{
    public LogDeepWorkCommandValidator()
    {
        RuleFor(x => x.Hours)
            .GreaterThanOrEqualTo(0).WithMessage("Hours must be at least 0.")
            .LessThanOrEqualTo(24).WithMessage("Hours must not exceed 24.");
    }
}
