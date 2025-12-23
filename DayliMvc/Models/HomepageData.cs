namespace DayliMvc.Models;

public class HomepageData
{
    public required WeatherDataSimple Weather { get; set; }
    public required CalendarDataSimple Calendar { get; set; }
}