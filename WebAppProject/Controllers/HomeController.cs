using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppProject.Models;
using WebAppProject.Services;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITokenService tokenService, ILogger<HomeController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Hi()
        {
            return Content("Hi there!");
        }

        public async Task<IActionResult> CallApiProject()
        {
            var data = new List<WeatherData>();

            using (var client = new HttpClient())
            {
                var tokenResponse = await _tokenService.GetToken("api1");

                client
                  .SetBearerToken(tokenResponse.AccessToken);

                var result = await client
                  .GetAsync("https://localhost:6001/weatherforecast");

                if (result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsStringAsync().Result;

                    data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
                    string s = JsonConvert.SerializeObject(data);

                    return Content(s);
                }
                else
                {
                    throw new Exception("Unable to get content");
                }

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return Content("Error occured!");
        }
    }
}
