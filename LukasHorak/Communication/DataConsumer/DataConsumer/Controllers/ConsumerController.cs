using DOM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace Consumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly IHttpClientFactory _httpfactory;

        public ConsumerController(ILogger<ConsumerController> logger, IHttpClientFactory httpfactory)
        {
            _logger = logger;
            _httpfactory = httpfactory;
        }
        /*
        [HttpGet(Name = "GetWeather")]
        public ActionResult<object> GetWeather()
        {
            var client = _httpfactory.CreateClient();

            var response = client.GetAsync("https://localhost:7134/WeatherForecast").Result;
            var content = response.Content.ReadFromJsonAsync<object>().Result;

            return content;
        }*/

        [Route("RecipeOfTheDay")]
        [HttpGet]
        public ActionResult<object> GetRecipe()
        {
            var client = _httpfactory.CreateClient();

            var response = client.GetAsync("https://localhost:7134/WeatherForecast/Single").Result;
            var recipe = response.Content.ReadFromJsonAsync<CookingRecipe>().Result;

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Created {recipe}");
                return recipe;
            }

            return response.StatusCode;
        }

        [Route("AllRecipes")]
        [HttpGet]
        public ActionResult<object> GetAll()
        {
            var client = _httpfactory.CreateClient();

            var response = client.GetAsync("https://localhost:7134/WeatherForecast/All").Result;
            var recipe = response.Content.ReadFromJsonAsync<CookingRecipe[]>().Result;

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Created {recipe}");
                return recipe;
            }

            return response.StatusCode;
        }


        //do parameter
        [HttpPost]
        public ActionResult<object> PostRecipe(CookingRecipe recipe)
        {
            var client = _httpfactory.CreateClient();

            var paramDict = new Dictionary<string, string>()
            {
                {"Recipe" ,JsonSerializer.Serialize(recipe) },
                {"name", "bordel" }
            };

            var uri = "https://localhost:7134/WeatherForecast";
            var uriWithParams = QueryHelpers.AddQueryString(uri, paramDict);

            var response = client.PostAsJsonAsync(uriWithParams, recipe).Result;
            var responseRecipe = response.Content.ReadFromJsonAsync<CookingRecipe>().Result;

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Created {responseRecipe}");
                return recipe;
            }

            return response.StatusCode;
        }
    }
}
