using System;
using System.Collections.Generic;
using System.Linq;
using domain.repository;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        IAccountService AccountService;
        IOrderService OrderService;
        public SampleDataController(IAccountService accountService, IOrderService orderService)
        {
            AccountService = accountService;
            OrderService = orderService;
        }
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var user = AccountService.GetAdminUsers();
            var orderNum = OrderService.GetOrderNum("");
            var rng = new Random();
            return Enumerable.Range(1, 9).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
