using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        IEnumerable<DishDto> GetAll(int restaurantId);
        DishDto Get(int restaurantId, int dishId);
        int Create(int restaurantId, CreateDishDto createDishDto);
        public void Update(int restaurantId, int dishId, UpdateDishDto updateDishDto);
        public void Delete(int restaurantId, int dishId);
        public void DeleteAll(int restaurantId);
    }
}
