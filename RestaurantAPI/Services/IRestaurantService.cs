using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GeyById(int id);
        public bool Delete(int id);
        public bool Update(int id, UpdateRestaurantDto dto);
    }
}