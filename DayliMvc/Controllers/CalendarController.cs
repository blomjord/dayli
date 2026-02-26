using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

using DayliMvc.Services;
using DayliMvc.Models.CalendarData;

using System.Runtime.CompilerServices;

namespace DayliMvc.Controllers;

public class CalendarController : Controller
{
    public async Task<IActionResult> Index()
    {
        var TaskCalendar = await CalendarService.GetCalendarEvents(numberOfDays: 90);
        return View(
            new CalendarDataFront
            {
                Events = TaskCalendar
            }
        );
    }
}