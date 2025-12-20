using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace DayliMvc.Controllers;

public class CalendarController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}