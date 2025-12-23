using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DayliMvc.Models;
using System.Security.Cryptography.X509Certificates;
using DayliMvc.Services;

namespace DayliMvc.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        var TaskWeather = WeatherService.CallWeatherAPISimple();
        var TaskCalendar = CalendarService.CallCalendarAPISimple();
        await Task.WhenAll(TaskWeather, TaskCalendar);
        
        return View(new HomepageData
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
