using System.Security.Principal;
using CupcakeMVC.Controllers;
using CupcakeMVC.Models.Entities;
using CupcakeMVC.Models.ViewModels;
using CupcakeMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace CupcakeMVCTests;

public class CupcakeControllerTests
{
    private readonly Mock<ICupcakeRepository> _mockRepo;
    private readonly CupcakeController _controller;

    public CupcakeControllerTests()
    {
        _mockRepo = new Mock<ICupcakeRepository>();
        _controller = new CupcakeController(_mockRepo.Object);
    }
    
    // Create
    [Fact]
    public void Create_ReturnsAViewResult_WithACupcakeViewModel()
    {
        // Arrange
        var cupcakeViewModel = new CupcakeEditViewModel();
        _mockRepo.Setup(repo => repo.GetCupcakeEditViewModel()).Returns(cupcakeViewModel);

        // Act
        var result = _controller.Create();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<CupcakeEditViewModel>(viewResult.Model);
        Assert.Equal(cupcakeViewModel, model);
    }

    [Fact]
    public void Create_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        // Arrange
        var cupcakeViewModel = new CupcakeEditViewModel
            { Name = "Strawberry Dream", Description = "Description", SizeId = 1, CategoryId = 1 };
        var cupcake = new Cupcake
        {
            Id = cupcakeViewModel.Id,
            Name = cupcakeViewModel.Name,
            Description = cupcakeViewModel.Description,
            SizeId = cupcakeViewModel.SizeId,
            CategoryId = cupcakeViewModel.Id
        };
        _mockRepo.Setup(repo => repo.Save(cupcake, It.IsAny<IPrincipal>()));
        
        // If TempData in controller.
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;

        // Act
        var result = _controller.Create(cupcakeViewModel);

        // Assert
        var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToAction.ActionName);
        Assert.Equal("Cupcake with name 'Strawberry Dream' created successfully", _controller.TempData["message"]);
    }
    
    [Fact]
    public void Create_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("error", "some error");

        // Act
        var result = _controller.Create(new CupcakeEditViewModel());

        // Assert
        Assert.IsType<ViewResult>(result);
    }
    
    [Fact]
    public void Create_ReturnsRedirectToActionResult_WhenExceptionIsThrown()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.Save(
            It.IsAny<Cupcake>(), It.IsAny<IPrincipal>()
        )).Throws(new Exception());
        
        // If TempData in controller.
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;
        
        // Act 
        var result = _controller.Create(new CupcakeEditViewModel());
        
        // Assert
        var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToAction.ActionName);
        Assert.Equal("Obs something went wrong", _controller.TempData["message"]);
    }
}