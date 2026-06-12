// src/Muhasabaa.Infrastructure/Services/IdentityService.cs
using ErrorOr;
using Muhasabaa.Application.Common.DTOs;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    public Task<ErrorOr<ApplicationUser>> CreateUserAsync(string name, string email, string password, Gender? gender)
        => throw new NotImplementedException();

    public Task<ErrorOr<ApplicationUser>> ValidateCredentialsAsync(string email, string password)
        => throw new NotImplementedException();
}


