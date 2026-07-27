using FluentValidation;

namespace Muhasabaa.Application.Spiritual.Commands.LogQiyam;

public class LogQiyamCommandValidator : AbstractValidator<LogQiyamCommand>
{
    public LogQiyamCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId must not be empty.");
    }
}
