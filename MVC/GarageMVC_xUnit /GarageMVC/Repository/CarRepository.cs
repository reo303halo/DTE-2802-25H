using GarageMVC.Models.Entities;

namespace GarageMVC.Repository;

public class CarRepository : ICarRepository
{
    private static List<Car> Cars { get; }

    static CarRepository()
    {
        Cars =
        [
            new Car { LicenseNumber = "AX32968", Make = "Chevrolet", Model = "Camaro", Year = 1981 },
            new Car { LicenseNumber = "HF27343", Make = "Mazda", Model = "Mazda 6", Year = 2016 },
            new Car { LicenseNumber = "YZ97000", Make = "Hyundai", Model = "Sanfa Fe", Year = 2007 }
        ];
    }

    public IEnumerable<Car> GetCars()
    {
        return Cars;
    }

    public Task Save(Car car)
    {
        Cars.Add(car);
        return Task.CompletedTask;
    }
    
}