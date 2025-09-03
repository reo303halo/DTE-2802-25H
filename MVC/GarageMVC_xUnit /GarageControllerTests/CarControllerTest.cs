using GarageMVC.Controllers;
using GarageMVC.Models.Entities;
using GarageMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GarageControllerTests;

public class CarControllerTest
{
    private readonly Mock<ICarRepository> _mockRepo;
    private readonly CarController _carController;


    public CarControllerTest()
    {
        _mockRepo = new Mock<ICarRepository>();
        _carController = new CarController(_mockRepo.Object);
    }

    [Fact]
    public void Index_ReturnsAViewResult_WithAListOfCars()
    {
        // Arrange: Works as a setup
        var cars = new List<Car> { new Car(), new Car(), new Car(), new Car() };
        _mockRepo.Setup(repo => repo.GetCars()).Returns(cars);
        
        // Act: Call the method from the controller, and keeps the result
        var result = _carController.Index();
        
        // Assert: Tests the results with expected result
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<List<Car>>(viewResult.ViewData.Model);
        Assert.Equal(4, model.Count);
    }
    
    // Create: GET
    [Fact]
    public void Create_ReturnsAViewResult()
    {
        // Act
        var result = _carController.Create();
        
        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    // Create: POST
    [Fact]
    public void Create_Post_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var car = new Car { LicenseNumber = "YN12312", Make = "Chevrolet", Model = "Silverado", Year = 2000};
        _mockRepo.Setup(repo => repo.Save(It.IsAny<Car>())).Verifiable();
        
        // Act
        var result = _carController.Create(car);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockRepo.Verify(repo => repo.Save(It.IsAny<Car>()), Times.Once);
    }

    [Fact]
    public void Create_Post_InvalidModel_ReturnsView()
    {
        // Arrange
        _carController.ModelState.AddModelError("Make", "Required");
        var car = new Car { LicenseNumber = "YN12312", Make = "", Model = "Silverado", Year = 2000};
        
        // Act
        var result = _carController.Create(car);
        
        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Null(viewResult.ViewData.Model);
    }

    [Fact]
    public void Create_Post_ReturnsViewResult_WhenExceptionIsThrown()
    {
        // Arrange
        var car = new Car();
        _mockRepo.Setup(repo => repo.Save(It.IsAny<Car>())).Throws(new Exception());
        
        // Act
        var result = _carController.Create(car);
        
        // Assert
        Assert.IsType<RedirectToActionResult>(result);
    }
}

