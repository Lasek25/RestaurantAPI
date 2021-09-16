using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Services
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "McDonalds",
                    Description = "McDonald's is an American corporation that operates one of the largest chains of fast food restaurants in the world.",
                    Category = "Fast food",
                    HasDelivery = false,
                    ContactEmail = "contact@mcdonalds.com",
                    Address = new Address()
                    {
                        City = "Warsaw",
                        Street = "Boczna",
                        Number = "35",
                        PostCode = "00-006"
                    },
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Big Mac",
                            Price = 11.99m,
                            Description = 
                            "Mouthwatering perfection starts with two 100% pure beef patties and Big Mac® sauce sandwiched " +
                            "between a sesame seed bun. It’s topped off with pickles, crisp shredded lettuce, finely chopped onion " +
                            "and American cheese for a 100% beef burger with a taste like no other. It contains no artificial flavors, " +
                            "preservatives or added colors from artificial sources"
                        },
                        new Dish()
                        {
                            Name = "Chicken McNuggets - 4 pcs",
                            Price = 9.99m,
                            Description = 
                            "Our tender, juicy Chicken McNuggets® are made with 100% white meat chicken and no artificial colors," +
                            " flavors or preservatives"
                        }
                    }
                },
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast food",
                    HasDelivery = true,
                    ContactEmail = "contact@kfc.com",
                    Address = new Address()
                    {
                        City = "Warsaw",
                        Street = "Wojska Polskiego",
                        Number = "97",
                        PostCode = "00-012"
                    },
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Twister",
                            Price = 10m,
                            Description =
                            "We wrapped the tender chicken fillets in crispy spicy or original breading with succulent lettuce, " +
                            "tomato slices and tender sauce and wrapped it in a wheat tortilla and toasted it."
                        },
                        new Dish()
                        {
                            Name = "Double Chefburger",
                            Price = 15m,
                            Description =
                            "New Chefburger now with double chicken! Double your enjoyment! Two juicy chicken fillets, " +
                            "tomato, fresh salad, cheese, sauce Caesar and appetizing bun."
                        },
                        new Dish()
                        {
                            Name = "De Luxe Cheeseburger",
                            Price = 5.5m,
                            Description =
                            "Herby mustard sauce, ketchup, juicy chicken fillet in original breading, onion, cheddar cheese, " +
                            "pickles on a corn bun with fresh salad and tomatoes."
                        }
                    }
                }
            };
            return restaurants;
        }
    }
}
