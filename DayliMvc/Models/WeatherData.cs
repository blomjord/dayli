// Models/WeatherData.cs
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace DayliMvc.Models;

public class WeatherData
{
    public int? userId { get; set; }
    public int? id { get; set; }
    public string? title { get; set;}
    public bool? completed { get; set; }
}


