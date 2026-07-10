using FluentValidation;

namespace Muhasabaa.Application.Physical.Commands;

public class LogSleepCommandValidator : AbstractValidator<LogSleepCommand>
{
    public LogSleepCommandValidator()
    {
        RuleFor(x => x.Hours)
            .GreaterThanOrEqualTo(0).WithMessage("Hours must be at least 0.")
            .LessThanOrEqualTo(24).WithMessage("Hours must not exceed 24.");
    }
}
