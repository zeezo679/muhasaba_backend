using FluentValidation;

namespace Muhasabaa.Application.Mental.Commands;

public class LogScreenTimeCommandValidator : AbstractValidator<LogScreenTimeCommand>
{
    public LogScreenTimeCommandValidator()
    {
        RuleFor(x => x.Hours)
            .GreaterThanOrEqualTo(0).WithMessage("Hours must be at least 0.")
            .LessThanOrEqualTo(24).WithMessage("Hours must not exceed 24.");
    }
}
