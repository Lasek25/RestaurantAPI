using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public int MinimumRestaurantsNumber { get; }
        public CreatedMultipleRestaurantsRequirement(int minimumRestaurantsNumber)
        {
            MinimumRestaurantsNumber = minimumRestaurantsNumber;
        }
    }
}
