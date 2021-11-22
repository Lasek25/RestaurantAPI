using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll(Query query);
        RestaurantDto GeyById(int id);
        public void Delete(int id);
        public void Update(int id, UpdateRestaurantDto dto);
    }
}