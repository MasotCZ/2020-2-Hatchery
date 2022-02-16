using Microsoft.AspNetCore.Mvc;

namespace DependecnyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IPrintable _printable;
        private readonly IPrintable _printable2;
        private readonly INumber _number;
        private readonly IPrinterDependency _printerDependency;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IPrintable printable,
            IPrintable printable2,
            INumber number,
            IPrinterDependency printerDependency,
            IServiceProvider serviceProvider)
        {
            var printable3 = serviceProvider.GetService<IPrintable>();

            _serviceProvider = serviceProvider;
            _logger = logger;
            _printable = printable;
            _printable2 = printable;
            _number = number;
            _printerDependency = printerDependency;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //testing life cycles
            //singleton
            _logger.LogInformation($"numero: {_number.GetNumber().ToString()}");
            //scoped IPrintable
            _logger.LogInformation($"printer: {_printable.Print()}");
            //scoped IPrintable
            _logger.LogInformation($"printer2: {_printable2.Print()}");
            //transient
            _logger.LogInformation($"dependency solo: {_printerDependency.GetMessage()}");

            //with service provider
            _logger.LogInformation($"service: {_serviceProvider.GetService<IPrintable>()}");
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}