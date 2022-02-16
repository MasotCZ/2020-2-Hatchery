using DOM;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private static readonly List<CookingRecipe> Recipes = new List<CookingRecipe>{
            new CookingRecipe(
                "Omacka",
                new Ingredient[3]{
                    new Ingredient("maso", 10),
                    new Ingredient("mrkev", 1),
                    new Ingredient("brambor", 3)
                }),
            new CookingRecipe(
                "Chleba s maslem",
                new Ingredient[2]{
                    new Ingredient("chleba", 1),
                    new Ingredient("maslo", 1)
                })
            };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeather()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        */

        [Route("Single")]
        [HttpGet]
        public ActionResult<CookingRecipe> GetRecipeOfTheDay()
        {
            var rnd = new Random();
            return Recipes[rnd.Next(0, Recipes.Count)];
        }

        [Route("All")]
        [HttpGet]
        public ActionResult<CookingRecipe[]> GetAll()
        {
            return Recipes.ToArray();
        }

        [HttpPost]
        public ActionResult<CookingRecipe> PostRecipe(CookingRecipe recipe, string name)
        {
            if (recipe is null)
            {
                return BadRequest();
            }

            recipe.Name = name;
            Recipes.Add(recipe);

            return recipe;
        }
    }
}