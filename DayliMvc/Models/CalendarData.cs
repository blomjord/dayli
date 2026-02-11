#nullable enable
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace DayliMvc.Models;

public class CalendarDataFront
{
    public int? userId { get; set; }
    public int? id { get; set; }
    public string? title { get; set; }
    public bool? completed { get; set; }
}

public class CalendarDataPage
{
    public int? userId { get; set; }
}

