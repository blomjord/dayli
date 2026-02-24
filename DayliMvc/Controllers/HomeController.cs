using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using DayliMvc.Models;
using DayliMvc.Services;
using Google.Apis.Calendar.v3.Data;

namespace DayliMvc.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        var TaskWeather = WeatherService.GetWeatherPointData();
        var TaskCalendar = CalendarService.GetCalendarDailyEvents(1);
        await Task.WhenAll(TaskWeather, TaskCalendar);

        return View(
            new HomepageData
                {
                    Weather = await TaskWeather,
                    Calendar = await TaskCalendar
                });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
