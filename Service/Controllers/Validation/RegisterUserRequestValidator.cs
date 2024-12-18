using FluentValidation;
using Service.Controllers.Users.Entities;

namespace Service.Controllers.Validation;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .Matches(@"[\w_]+")
            .WithMessage("Login is required");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("Name is required");
        
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("role id is required");
        
    }
}