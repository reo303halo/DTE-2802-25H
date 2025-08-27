using SimpleMVC.Models.Entities;

namespace SimpleMVC.Repository;

public class CarRepository : ICarRepository
{
    private static List<Car> Cars { get; }

    static CarRepository()
    {
        Cars = new List<Car>
        {
            new Car { CarId = "AX32968", Make = "Chevrolet", Model = "Camaro", Year = 1981 },
            new Car { CarId = "HF27343", Make = "Mazda", Model = "Mazda 6", Year = 2016 },
            new Car { CarId = "YZ97000", Make = "Hyundai", Model = "Santa Fe", Year = 2007 }
        };
    }
    
    
    public IEnumerable<Car> GetAll()
    {
        return Cars;
    }

    public Task Save(Car car)
    {
        Cars.Add(car);
        return Task.CompletedTask;
    }
}