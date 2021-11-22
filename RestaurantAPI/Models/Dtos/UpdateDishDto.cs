using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class UpdateDishDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; } = -1;
    }
}
