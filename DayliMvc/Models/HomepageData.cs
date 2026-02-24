using DayliMvc.Models.WeatherData;
using DayliMvc.Models.CalendarData;
using Google.Apis.Calendar.v3.Data;

namespace DayliMvc.Models;

public class HomepageData
{
    public required WeatherDataFront Weather { get; set; }
    public List<Event> Calendar { get; set; } = new();
}