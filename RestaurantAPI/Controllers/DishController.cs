using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    [Authorize]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<DishDto>> GetAll([FromRoute] int restaurantId)
        {
            var dishesDtos = _dishService.GetAll(restaurantId);
            return Ok(dishesDtos);
        }

        [HttpGet("{dishId}")]
        [AllowAnonymous]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishDto = _dishService.Get(restaurantId, dishId);
            return Ok(dishDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishDto createDishDto)
        {
            var newDishId = _dishService.Create(restaurantId, createDishDto);
            return Created($"api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }

        [HttpPut("{dishId}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Update([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] UpdateDishDto updateDishDto)
        {
            _dishService.Update(restaurantId, dishId, updateDishDto);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = "CreatedRestaurantsOver")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteAll([FromRoute] int restaurantId)
        {
            _dishService.DeleteAll(restaurantId);
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _dishService.Delete(restaurantId, dishId);
            return NoContent();
        }
    }
}
