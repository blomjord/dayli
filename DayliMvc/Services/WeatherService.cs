// Services/WeatherServices.cs
using DayliMvc.Models;
using System.Net.Http;
using System.Text.Json;

namespace DayliMvc.Services;

public class WeatherService
{

    public static async Task<WeatherData?> CallWeatherAPI()
    {
        using HttpClient httpClient = new HttpClient();

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            WeatherData? weatherData = JsonSerializer.Deserialize<WeatherData>(
                responseContent, 
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return weatherData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e}");
            return null;
        }
    }

}

// SMHI API info here ---> https://opendata.smhi.se/metobs/introduction

// Test API Response: https://jsonplaceholder.typicode.com/todos/1