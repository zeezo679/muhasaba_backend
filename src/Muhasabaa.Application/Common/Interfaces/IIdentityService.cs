// src/Muhasabaa.Application/Common/Interfaces/IIdentityService.cs
using ErrorOr;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<ErrorOr<ApplicationUser>> CreateUserAsync(string name, string email, string password, Gender? gender);
    Task<ErrorOr<ApplicationUser>> ValidateCredentialsAsync(string email, string password);
}

