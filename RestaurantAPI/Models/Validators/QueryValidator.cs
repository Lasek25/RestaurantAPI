using FluentValidation;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Models.Validators
{
    public class QueryValidator : AbstractValidator<Query>
    {
        private int[] allowedPageSizes = new[] { 3, 5, 10 };
        private string[] allowedSortBy = new[] 
        { 
            nameof(Restaurant.Name),
            nameof(Restaurant.Category),
            nameof(Restaurant.HasDelivery)
        };
        public QueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Value of Page Number parameter cannot be lower than 1");
            RuleFor(x => x.PageSize)
                .Custom((value, context) =>
                {
                    if(!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure(
                            "PageSize", $"Page Size value must be one of: [{string.Join(",", allowedPageSizes)}]"
                        );
                    }
                });
            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortBy.Contains(value))
                .WithMessage($"Value of Sort By parameter must be empty or be one of: [{string.Join(",", allowedSortBy)}]");
                       
        }
    }
}
