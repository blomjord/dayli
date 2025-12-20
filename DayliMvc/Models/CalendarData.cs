using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

namespace DayliMvc.Models;

public class CalendarData
{
    public int? UserId { get; set; }
    public int? Id { get; set; }
    public string? Title { get; set;}
    public bool? Completed { get; set; }
}
