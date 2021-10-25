using FluentValidation;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Models.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        public CreateRestaurantDtoValidator(RestaurantDbContext dbContext)
        {
            var nameMaxLength = 25;
            var cityMaxLength = 50;
            var streetMaxLength = 50;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("'Name' field cannot be empty")
                .MaximumLength(nameMaxLength)
                .WithMessage($"Max. length of the 'Name' field is {nameMaxLength} characters");
            RuleFor(x => x.ContactEmail)
                .EmailAddress()
                .WithMessage("Incorrect 'Contact Email' field format");
            RuleFor(x => x.ContactNumber)
                .Matches(@"^\d{9}$")
                .WithMessage("Incorrect 'Contact Number' field format");
            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("'City' field cannot be empty")
                .MaximumLength(cityMaxLength)
                .WithMessage($"Max. length of the 'City' field is {cityMaxLength} characters");
            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("'Street' field cannot be empty")
                .MaximumLength(streetMaxLength)
                .WithMessage($"Max. length of the 'Street' field is {streetMaxLength} characters");
        }
    }
}
