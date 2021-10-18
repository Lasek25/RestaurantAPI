﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public RestaurantService(
            RestaurantDbContext dbContext, 
            IMapper mapper, 
            ILogger<RestaurantService> logger, 
            IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public RestaurantDto GeyById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);
            if (restaurant is null) throw new NotFoundException("Restaurant not found");
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto, int userId)
        {
            var newRestaurant = _mapper.Map<Restaurant>(dto);
            newRestaurant.CreatedById = userId;
            _dbContext.Restaurants.Add(newRestaurant);
            _dbContext.SaveChanges();
            return newRestaurant.Id;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            _logger.LogError($"Restaurant with id {id} DELETE action invoked");
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(
                user, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateRestaurantDto dto, ClaimsPrincipal user)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            var authorizationResult = _authorizationService.AuthorizeAsync(
                user, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            if (dto.Name is not null)
                restaurant.Name = dto.Name;
            if (dto.Description is not null)
                restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }
    }
}
