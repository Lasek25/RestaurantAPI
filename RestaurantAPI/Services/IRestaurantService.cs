using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto, int userId);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GeyById(int id);
        public void Delete(int id, ClaimsPrincipal user);
        public void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user);
    }
}