// Services/WeatherServices.cs
#nullable enable

using DayliMvc.Models;
using DayliMvc.Models.WeatherData;
using System.Text.Json;

namespace DayliMvc.Services;

public class WeatherService
{
    private static string baseUrl = $"https://opendata-download-metfcst.smhi.se/api/category/snow1g/version/1";
    private static string latitude = "59.849722";
    private static string longitude = "17.638889";
    private static string pointDataUrl = $"{baseUrl}/geotype/point/lon/{longitude}/lat/{latitude}/data.json";

    public static async Task<WeatherDataFront?> GetWeatherPointData()
    {
        using HttpClient httpClient = new HttpClient();
        try
        {
            HttpResponseMessage pointDataResponse = await httpClient.GetAsync($"{pointDataUrl}");
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
// API Entry: https://opendata.smhi.se/metfcst/snow1gv1/