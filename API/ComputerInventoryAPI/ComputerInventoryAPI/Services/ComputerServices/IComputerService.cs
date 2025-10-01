using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Models.Entities;

namespace ComputerInventoryAPI.Services.ComputerServices;

public interface IComputerService
{
    Task<IEnumerable<ComputerDto>> GetAllComputers();

    ComputerDto? GetComputer(int id);

    ComputerDto? GetComputerByName(string name);

    Task Save(Computer computer);

    Task Delete(int id);
}