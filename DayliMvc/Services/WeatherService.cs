// Services/WeatherServices.cs
using DayliMvc.Models;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Xml;

namespace DayliMvc.Services;

public class WeatherService
{
    private static string BaseUrl = $"https://opendata-download-metobs.smhi.se/api/version/latest/parameter";
    private static string ParamTemperatureInstantaneous = "1";  // Once per hour
    private static string ParamPrecipitation = "17";            // Twice per day; 06:00, 18:00
    private static string ParamWindspeed = "4";                 // Once per hour, average
    private static string ParamWindDirection = "3";             // Once per hour, average
    private static string ParamSnowDepth = "8";                 // Once per day; 06:00
    
    private static string StationUppsalaAut = "97510";
    private static string StationHarnosand = "97510";

    // https://opendata-download-metobs.smhi.se/api/version/latest/parameter/1/station/97510/period/latest-day/data.json";

    public static async Task<WeatherDataSimple?> CallWeatherAPISimple()
    {
        using HttpClient httpClient = new HttpClient();
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{BaseUrl}/{ParamTemperatureInstantaneous}/station/{StationUppsalaAut}/period/latest-day/data.json");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();

            //WeatherDataSimple? weatherData = new WeatherDataSimple();
            var weatherData = JsonSerializer.Deserialize<WeatherDataSimple>(responseContent);
            long updated = weatherData.updated;

            // Convert UNIX Epoch to date time
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(updated);
            var dateTime = dateTimeOffset.DateTime;
            Console.WriteLine($"Time is {dateTime}");
            //weatherData.time = dateTime.ToString();
            //weatherData.time = "1";
            //weatherData.value[0].time = "1";
            weatherData.value[0].date = 1;
            return weatherData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e}");
            return null;
        }
    }

    public static async Task<WeatherDataDetailed?> CallWeatherAPIDetailed()
    {
        using HttpClient httpClient = new HttpClient();
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            WeatherDataDetailed? weatherData = JsonSerializer.Deserialize<WeatherDataDetailed>(
                responseContent, new JsonSerializerOptions
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
// https://opendata-download-metobs.smhi.se/api/version/latest/parameter/1/station/159880/period/latest-day/data.json