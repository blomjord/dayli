// Controllers/WeatherController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

using DayliMvc.Services;
using Microsoft.VisualBasic;
using DayliMvc.Models;

namespace DayliMvc.Controllers;

public class WeatherController : Controller
{
    public async Task<IActionResult> Index()
    {
        WeatherData? APIResponse = await WeatherService.CallWeatherAPI();
        if (APIResponse == null)
        {
            return View(null);
        }
        return View(APIResponse);
    }
}