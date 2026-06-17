// src/Muhasabaa.Application/Auth/Refresh/RefreshCommandValidator.cs
using FluentValidation;

namespace Muhasabaa.Application.Auth.Refresh;

public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
{
    public RefreshCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}