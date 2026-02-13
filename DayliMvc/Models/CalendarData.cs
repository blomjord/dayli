#nullable enable

using Google.Apis.Calendar.v3.Data;

namespace DayliMvc.Models.CalendarData;
public class CalendarDataFront
{
    public List<Event> Events { get; set; } =  new();
}

public class CalendarDataPage
{
    public int? userId { get; set; }
}



