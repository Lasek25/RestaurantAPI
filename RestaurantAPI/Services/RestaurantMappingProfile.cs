using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostCode, c => c.MapFrom(s => s.Address.PostCode))
                .ForMember(m => m.Number, c => c.MapFrom(s => s.Address.Number));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                    c => c.MapFrom(dto => new Address()
                        { City = dto.City, Street = dto.Street, PostCode = dto.PostCode, Number = dto.Number }));

            CreateMap<CreateDishDto, Dish>();
        }
    }
}
