using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto createDishDto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var newDish = _mapper.Map<Dish>(createDishDto);
            newDish.RestaurantId = restaurantId;
            _dbContext.Add(newDish);
            _dbContext.SaveChanges();
            return newDish.Id;
        }

        public void Delete(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = GetDishById(restaurantId, dishId);
            _dbContext.Remove(dish);
            _dbContext.SaveChanges();
        }

        public void DeleteAll(int restaurantId)
        {
            var dishes = GetRestaurantById(restaurantId).Dishes;
            _dbContext.RemoveRange(dishes);
            _dbContext.SaveChanges();
        }

        public DishDto Get(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = GetDishById(restaurantId, dishId);
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }

        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var dishes = GetRestaurantById(restaurantId).Dishes;
            var dishesDtos = _mapper.Map<List<DishDto>>(dishes);
            return dishesDtos;
        }

        public void Update(int restaurantId, int dishId, UpdateDishDto updateDishDto)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = GetDishById(restaurantId, dishId);
            if (updateDishDto.Description is not null)
                dish.Description = updateDishDto.Description;
            if (updateDishDto.Price >= 0)
                dish.Price = updateDishDto.Price;
            _dbContext.SaveChanges();
        }

        private Dish GetDishById(int restaurantId, int dishId)
        {
            var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish?.RestaurantId != restaurantId)
            {
                throw new NotFoundException("Dish not found");
            }
            return dish;
        }

        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);
            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }
            return restaurant;
        }
    }
}
