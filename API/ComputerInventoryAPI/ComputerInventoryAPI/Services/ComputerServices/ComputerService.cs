using ComputerInventoryAPI.Data;
using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventoryAPI.Services.ComputerServices;

public class ComputerService : IComputerService
{
    private readonly ApplicationDbContext _db;

    public ComputerService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ComputerDto>> GetAllComputers()
    {
        try
        {
            var computers = _db.Computer
                .Include(c => c.Brand)
                .Include(c => c.OperatingSystem)
                .ToList();

            var returnComputers = computers.Select(c => new ComputerDto
            {
                Id = c.Id,
                Name = c.Name,
                Processor = c.Processor,
                Ram = c.Ram,
                Storage = c.Storage,
                Brand = new BrandDto
                {
                    Id = c.Brand.Id,
                    Name = c.Brand.Name
                },
                OperatingSystem = new OperatingSystemDto
                {
                    Id = c.OperatingSystem.Id,
                    Name = c.OperatingSystem.Name,
                    Version = c.OperatingSystem.Version
                }
            }).ToList();

            return returnComputers;
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);

            return new List<ComputerDto>();
        }
    }

    public ComputerDto? GetComputer(int id)
    {
        try
        {
            var computer = _db.Computer
                .Where(c => c.Id == id)
                .Include(c => c.Brand)
                .Include(c => c.OperatingSystem)
                .FirstOrDefault();

            if (computer == null) return null;
            var computerDto = new ComputerDto
            {
                Id = computer.Id,
                Name = computer.Name,
                Processor = computer.Processor,
                Ram = computer.Ram,
                Storage = computer.Storage,
                Brand = new BrandDto
                {
                    Id = computer.Brand.Id,
                    Name = computer.Brand.Name
                },
                OperatingSystem = new OperatingSystemDto
                {
                    Id = computer.OperatingSystem.Id,
                    Name = computer.OperatingSystem.Name,
                    Version = computer.OperatingSystem.Version
                }
            };

            return computerDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public ComputerDto? GetComputerByName(string name)
    {
        try
        {
            var computer = _db.Computer
                .Where(c => c.Name.ToLower() == name.ToLower())
                .Include(c => c.Brand)
                .Include(c => c.OperatingSystem)
                .FirstOrDefault();

            if (computer == null) return null;
            var computerDto = new ComputerDto
            {
                Id = computer.Id,
                Name = computer.Name,
                Processor = computer.Processor,
                Ram = computer.Ram,
                Storage = computer.Storage,
                Brand = new BrandDto
                {
                    Id = computer.Brand.Id,
                    Name = computer.Brand.Name
                },
                OperatingSystem = new OperatingSystemDto
                {
                    Id = computer.OperatingSystem.Id,
                    Name = computer.OperatingSystem.Name,
                    Version = computer.OperatingSystem.Version
                }
            };

            return computerDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Save(Computer computer)
    {
        var existingComputer = await _db.Computer.FindAsync(computer.Id);
        if (existingComputer != null)
        {
            _db.Entry(existingComputer).State = EntityState.Detached;
        }

        _db.Computer.Update(computer);
        await _db.SaveChangesAsync();
    }
    
    public async Task Delete(int id)
    {
        var computer = _db.Computer.FindAsync(id);
        
        _db.Computer.Remove(await computer ?? throw new InvalidOperationException($"No computer with id {id} found."));
        await _db.SaveChangesAsync();
    }
}
