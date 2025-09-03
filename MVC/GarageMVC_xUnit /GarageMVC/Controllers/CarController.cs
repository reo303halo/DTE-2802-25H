using GarageMVC.Models.Entities;
using GarageMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GarageMVC.Controllers;

public class CarController(ICarRepository repository) : Controller
{
    // GET : Read
    public IActionResult Index()
    {
        var cars = repository.GetCars();
        
        return View(cars);
    }
    
    // GET : Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST : Create
    [HttpPost]
    public IActionResult Create([Bind("LicenseNumber,Make,Model,Year")] Car car)
    {
        try
        {
            if (!ModelState.IsValid) return View();
            repository.Save(car);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
    }
}