using DayliMvc.Models.WeatherData;

namespace DayliMvc.Models;

public class HomepageData
{
    public required WeatherDataFront Weather { get; set; }
    public required CalendarDataSimple Calendar { get; set; }
}