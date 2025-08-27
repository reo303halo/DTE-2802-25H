using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Models.Entities;
using SimpleMVC.Repository;

namespace SimpleMVC.Controllers;

public class GarageController : Controller
{
    private readonly ICarRepository _repository;

    public GarageController(ICarRepository repository)
    {
        _repository = repository;
    }

    // Home/Index: GET
    public IActionResult Index()
    {
        var cars = _repository.GetAll();
        
        return View(cars);
    }

    // Create: GET
    public IActionResult Create()
    {
        return View();
    }

    // Create: POST
    [HttpPost]
    public IActionResult Create([Bind("CarId,Make,Model,Year")] Car car)
    {
        try
        {
            if (!ModelState.IsValid) return View();
            
            _repository.Save(car);
            TempData["message"] = $"{car.CarId} has been created!";
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            TempData["message"] = "Obs something went wrong!";
            return RedirectToAction("Index");
        }
    }
}