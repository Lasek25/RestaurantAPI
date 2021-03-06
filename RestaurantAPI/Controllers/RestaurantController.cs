using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<PagedResult<RestaurantDto>> GetAll([FromQuery] Query query)
        {
            var restaurantsDtos = _restaurantService.GetAll(query);
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GeyById(id);
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            // Not necessary if attribute [ApiController] exists
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _restaurantService.Create(dto);
            return Created($"api/restaurant/{id}", null);
        }
        
        [HttpDelete("{id}")]
        //[Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "AgeOver")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
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
