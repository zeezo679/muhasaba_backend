// src/Muhasabaa.Infrastructure/Services/IdentityService.cs
using ErrorOr;
using Muhasabaa.Application.Common.DTOs;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Muhasabaa.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IAppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public IdentityService(IAppDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public async Task<ErrorOr<ApplicationUser>> CreateUserAsync(string name, string email, string password, Gender? gender)
    {
        
        //refactor to factory method later
        var user = new ApplicationUser
        {
            UserName = email,
            Name = name,
            Email = email,
            ProfileImageUrl = null,
            CreatedAt = DateTime.UtcNow,
            Gender = gender,
        };
        
        var result = await _userManager.CreateAsync(user, password);
        
        if(!result.Succeeded)
        {
            var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
            return errors;
        }
        
        return user;
    }

    public async Task<ErrorOr<ApplicationUser>> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if(user is null)
            return Error.Unauthorized("InvalidCredentials", "Invalid email or password.");
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        
        if(!isPasswordValid)
            return Error.Unauthorized("InvalidCredentials", "Invalid email or password.");
        
        return user;
    }
}


