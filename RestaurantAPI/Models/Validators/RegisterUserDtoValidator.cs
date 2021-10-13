using FluentValidation;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email field cannot be empty")
                .EmailAddress()
                .WithMessage("Incorect email address");
            RuleFor(x => x.Password)
                //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$")
                .MinimumLength(6).WithMessage("Password must contain min. 6 characters")
                .Matches(@"(?=.*[a-z])").WithMessage("Password must contain min. one lower letter")
                .Matches(@"(?=.*[A-Z])").WithMessage("Password must contain min. one upper letter")
                .Matches(@"(?=.*\d)").WithMessage("Password must contain min. one digit");
                //.WithMessage("Password must contain min. 6 characters, min. one upper letter, min. one lower letter and min. one digit");
            RuleFor(x => x.ConfirmPassword)
                .Equal(y => y.Password)
                .WithMessage("The values of the Confirm Password and the Password fields must be equal");
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "This email address is already taken!");
                    }
                });
        }
    }
}
