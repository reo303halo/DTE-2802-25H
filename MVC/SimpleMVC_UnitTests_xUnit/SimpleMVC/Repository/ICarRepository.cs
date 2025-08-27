using SimpleMVC.Models.Entities;

namespace SimpleMVC.Repository;

public interface ICarRepository
{
    IEnumerable<Car> GetAll();

    Task Save(Car car);
}