// Models/WeatherData.cs
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace DayliMvc.Models;

public class WeatherDataSimple
{
    public long updated { get; set; }
    public List<Value> value { get; set; }
}
public class Value
{
    public long? date { get; set; }
    public string? value { get; set; }
    public string? quality { get; set;}
}

// This is not yet implemented.


public class WeatherDataDetailed
{
    public int? userId { get; set; }
    public int? id { get; set; }
    public string? title { get; set;}
    public bool? completed { get; set; }
}
