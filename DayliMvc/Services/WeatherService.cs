// Services/WeatherServices.cs
using DayliMvc.Models;
using DayliMvc.Models.WeatherData;
using System.Text.Json;

namespace DayliMvc.Services;

public class WeatherService
{
    private static string baseUrl = $"https://opendata-download-metfcst.smhi.se/api/category/snow1g/version/1";
    private static string createdTimeUrl = $"{baseUrl}/createdtime.json";
    private static string wantedLatitude = "57.996626";
    private static string wantedLongitude = "16.011977";
    private static string pointDataUrl = $"{baseUrl}/geotype/point/lon/${wantedLongitude}/lat/${wantedLatitude}/data.json";
    private static string testUrl = "https://opendata-download-metfcst.smhi.se/api/category/snow1g/version/1/geotype/point/lon/16.011977/lat/57.996626/data.json";

    public static async Task<WeatherDataFront?> GetWeatherPointData()
    {
        using HttpClient httpClient = new HttpClient();
        try
        {
            HttpResponseMessage pointDataResponse = await httpClient.GetAsync($"{testUrl}");
            pointDataResponse.EnsureSuccessStatusCode();
            string pointData = await pointDataResponse.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<WeatherDataFront>(pointData);
            return weatherData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e}");
            return null;
        }
    }
}