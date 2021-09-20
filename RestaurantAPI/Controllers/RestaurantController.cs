using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            // Not necessary if attribute [ApiController] exists
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var id = _restaurantService.Create(dto);
            return Created($"api/restaurant/{id}", null);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GeyById(id);
            return Ok(restaurant);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            // Not necessary if attribute [ApiController] exists
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            _restaurantService.Update(id, dto);
            return Ok();
        }
    }
}
