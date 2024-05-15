using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RulesSimulator.Models;

namespace RulesSimulator.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("Getfix")]
        public string Getfiix(Fix fix)
        {
            return fix.FixMsg;
        }
        public static string ReadJsonFile(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }
        /*[HttpGet("endpoint")]
        public IActionResult GetJsonFile()
        {
            string path = @"C:\\Users\\MHlaili\\Desktop\\json\\fix.json";
            var stream = System.IO.File.OpenRead(path);
            return File(stream, "application/json");
        }
        [HttpPost]
        [Route("Addfix")]
        public string Addfiix(Fix value)
        {
            string sname = value.Fname;
            string sid = value.Fid;
            string sfix = value.FixMsg;
           
            
            return value.FixMsg;

        }*/



    }
    
    public class Fix
    {
        public string Fname { get; set; }
        public string Fid { get; set; }
        public string FixMsg { get; set; }
    }



}
