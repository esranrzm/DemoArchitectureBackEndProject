using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Context.EntityFramework;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        SqlConnection con = new SqlConnection("Server=PRPC11;Database=DemoDb;Integrated Security=true;");
        SimpleContextDb context = new SimpleContextDb();

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
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Users", con);
            DataTable da = new DataTable();
            da.Clear();
            adp.Fill(da);

            context.Users.ToList();

            SqlCommand kmt = new SqlCommand("insert into Users(Name, ImageUrl, Password) Values(Name, ImageUrl, Password)", con);
            con.Open();
            kmt.ExecuteNonQuery();
            con.Close();

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