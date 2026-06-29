using FluentValidation;

namespace Muhasabaa.Application.Spiritual.Commands.LogQuran;

public class LogQuranCommandValidator : AbstractValidator<LogQuranCommand>
{
    public LogQuranCommandValidator()
    {
        RuleFor(x => x.Pages)
            .GreaterThanOrEqualTo(0).WithMessage("Pages must be at least 0.")
            .LessThanOrEqualTo(604).WithMessage("Pages must not exceed 604.");
    }
}
