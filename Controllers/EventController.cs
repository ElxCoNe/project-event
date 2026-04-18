using EventProject.Data;
using Microsoft.AspNetCore.Mvc;
using EventProject.Models;

namespace EventProject.Controllers;

public class EventController : Controller
{
    private readonly MySqlDbContext _db;

    public EventController(MySqlDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        var events = _db.events
            .Where(e => e.Status != "Deleted")
            .ToList();
        return View(events);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Event evt)
    {
        evt.CreatedAt = DateTime.UtcNow;
        _db.events.Add(evt);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var evt = _db.events.Find(id);
        if (evt == null)
            return NotFound();
        return View(evt);
    }

    [HttpPost]
    public IActionResult Edit(Event evt)
    {
        evt.UpdatedAt = DateTime.UtcNow;
        _db.events.Update(evt);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var evt = _db.events.Find(id);
        if (evt == null) 
            return NotFound();
    
        // Eliminado lógico, no borramos el registro
        evt.Status = "Deleted";
        evt.UpdatedAt = DateTime.UtcNow;
        _db.events.Update(evt);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ChangeStatus(int id, string newStatus)
    {
        var evt = _db.events.Find(id);
        if (evt == null)
            return NotFound();
    
        evt.Status = newStatus;
        evt.UpdatedAt = DateTime.UtcNow;
        _db.events.Update(evt);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}