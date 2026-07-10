using FluentValidation;

namespace Muhasabaa.Application.Physical.Commands;

public class LogGymCommandValidator : AbstractValidator<LogGymCommand>
{
    public LogGymCommandValidator()
    {
        RuleFor(x => x.Minutes)
            .GreaterThanOrEqualTo(0).WithMessage("Minutes must be at least 0.")
            .LessThanOrEqualTo(1440).WithMessage("Minutes must not exceed 1440.");
    }
}
