using GarageMVC.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GarageMVC.Controllers;

public class CarController : Controller
{
    private readonly IEnumerable<Car> _cars = 
    [
        new Car { LicenseNumber = "AX32968", Make = "Chevrolet", Model = "Camaro", Year = 1981 },
        new Car { LicenseNumber = "HF27343", Make = "Mazda", Model = "Mazda 6", Year = 2016 },
        new Car { LicenseNumber = "YZ97000", Make = "Hyundai", Model = "Sanfa Fe", Year = 2007 }
    ];
    
    // GET
    public IActionResult Index()
    {
        return View(_cars);
    }
}