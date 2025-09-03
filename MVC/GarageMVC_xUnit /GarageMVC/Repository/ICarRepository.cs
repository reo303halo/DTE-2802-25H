using GarageMVC.Models.Entities;

namespace GarageMVC.Repository;

public interface ICarRepository
{
    IEnumerable<Car> GetCars();
    Task Save(Car car);
}