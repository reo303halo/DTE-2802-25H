using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SimpleMVC.Controllers;
using SimpleMVC.Models.Entities;
using SimpleMVC.Repository;

namespace GarageControllerTests;

public class GarageControllerTest
{
    private readonly Mock<ICarRepository> _mockRepo;
    private readonly GarageController _controller;

    public GarageControllerTest()
    {
        _mockRepo = new Mock<ICarRepository>();
        _controller = new GarageController(_mockRepo.Object);
    }
    
    // Index
    [Fact]
    public void Index_ReturnsAViewResult_WithAListOfCars()
    {
        // Arrange: Works as a setup
        var cars = new List<Car> { new Car(), new Car(), new Car(), new Car() };
        _mockRepo.Setup(repo => repo.GetAll()).Returns(cars);

        // Act: Call the method from the controller, and keeps the result
        var result = _controller.Index();
        
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
        var result = _controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    // Create: POST
    [Fact]
    public void Create_Post_ValidModel_RedirectToIndex()
    {
        // Arrange
        var car = new Car { CarId = "YN12312", Make = "Toyota", Model = "Corolla", Year = 2000 };
        _mockRepo.Setup(repo => repo.Save(It.IsAny<Car>())).Verifiable();
        
        // If tempData is used in controller, add these 2 lines.
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;
        
        // Act
        var result = _controller.Create(car);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        _mockRepo.Verify(repo => repo.Save(It.IsAny<Car>()), Times.Once);
        Assert.Equal("YN12312 has been created!", _controller.TempData["message"]);
    }

    [Fact]
    public void Create_post_InvalidModel_ReturnsView()
    {
        // Arrange
        _controller.ModelState.AddModelError("Make", "Required");
        var car = new Car { CarId = "YN12312", Make = "", Model = "Corolla", Year = 2020 };
        
        // Act
        var result = _controller.Create(car);
        
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
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;
        
        // Act
        var result = _controller.Create(car);
        
        // Assert 
        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Obs something went wrong!", _controller.TempData["message"]);
    }
}