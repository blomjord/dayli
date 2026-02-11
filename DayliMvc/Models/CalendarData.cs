#nullable enable
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace DayliMvc.Models;

public class CalendarDataFront
{
    public int? UserId { get; set; }
}

public class CalendarDataDetailed
{
    public int? UserId { get; set; }
    public int? Id { get; set; }
    public string? Title { get; set;}
    public bool? Completed { get; set; }
}

