using DayliMvc.Models;
using System.Net.Http;
using System.Text.Json;

namespace DayliMvc.Services;

public class CalendarService
{
    public static async Task<CalendarDataSimple?> CallCalendarAPISimple()
    {
        using HttpClient httpClient = new HttpClient();

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            CalendarDataSimple? CalendarData = JsonSerializer.Deserialize<CalendarDataSimple>(
                responseContent, 
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return CalendarData;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e}");
            return null;
        }
    }
    public static async Task<CalendarDataDetailed?> CallCalendarAPIDetailed()
    {
        using HttpClient httpClient = new HttpClient();

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            CalendarDataDetailed? CalendarData = JsonSerializer.Deserialize<CalendarDataDetailed>(
                responseContent, 
                new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return CalendarData;
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