using DayliMvc.Models.WeatherData;

namespace DayliMvc.Models;

public class HomepageData
{
    public required WeatherDataFront Weather { get; set; }
    public required CalendarDataFront Calendar { get; set; }
}