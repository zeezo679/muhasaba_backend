using FluentValidation;

namespace Muhasabaa.Application.Spiritual.Commands.LogDhikr;

public class LogDhikrCommandValidator : AbstractValidator<LogDhikrCommand>
{
    public LogDhikrCommandValidator()
    {
        RuleFor(x => x.DhikrCount)
            .GreaterThanOrEqualTo(0).WithMessage("DhikrCount must be at least 0.")
            .LessThanOrEqualTo(10000).WithMessage("DhikrCount must not exceed 10000.");
    }
}
