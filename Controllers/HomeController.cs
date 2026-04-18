using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventProject.Models;
using EventProject.Data;

namespace EventProject.Controllers;

public class HomeController : Controller
{
    private readonly MySqlDbContext _db;

    public HomeController(MySqlDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string? category)
    {
        var events = _db.events
            .Where(e => e.Status != "Draft" && e.Status != "Deleted")
            .ToList();

        if (!string.IsNullOrEmpty(category))
        {
            events = events.Where(e => e.Category == category).ToList();
        }

        return View(events);
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