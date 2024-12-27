using InvoiceApp.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvoiceApp.UnitTests
{
    public class WeatherForecastTests
    {
        [Fact]
        public async Task GetWeatherForecast_ReturnSucessAndCorrectContentType()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5087/WeatherForecast");

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonSerializer.Deserialize<List<WeatherForecast>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(weatherForecast);
            Assert.IsType<List<WeatherForecast>>(weatherForecast);
        }
    }
}
