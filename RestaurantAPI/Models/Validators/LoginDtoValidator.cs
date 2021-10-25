using FluentValidation;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("'Email' field cannot be empty")
                .EmailAddress()
                .WithMessage("Incorrect email address");
        }

    }
}
