using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using ProductInventory.Controllers;
using ProductInventory.Models.Entities;
using ProductInventory.Models.ViewModel;
using ProductInventory.Repositories;

namespace ProductInventoryTests;

[TestClass]
public class ProductControllerTest
{
    private Mock<IProductRepository> _repository;
    private ProductController _controller;
    
    
    
    // INDEX TESTS
    [TestMethod]
    public void IndexReturnsNotNullResult()
    {
        // Arrange 
        _repository = new Mock<IProductRepository>();

        var fakeproducts = new List<Product>
        {
            new Product { Name = "Hammer", Price = 121.50m, CategoryId = 1, ManufacturerId = 1 },
            new Product { Name = "Angle grinder", Price = 1520.00m, CategoryId = 1, ManufacturerId = 1 },
            new Product { Name = "Milk", Price = 14.50m, CategoryId = 3, ManufacturerId = 3 },
            new Product { Name = "Meat", Price = 32.00m, CategoryId = 3, ManufacturerId = 3 },
            new Product { Name = "Bread", Price = 25.50m, CategoryId = 3, ManufacturerId = 3 }
        };
        
        _repository.Setup(x => x.GetProducts()).Returns(fakeproducts);
        var controller = new ProductController(_repository.Object);
        
        // Act
        var result = (ViewResult)controller.Index();
        
        // Assert
        CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Product));
        Assert.IsNotNull(result, "View Result is null");
        
        var products = result.ViewData.Model as List<Product>;
        Assert.AreEqual(5, products.Count, "Got wrong number of products");
    }
    
    [TestMethod]
    public void IndexReturnsAllProducts()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        var fakeproducts = new List<Product>
        {
            new Product { Name = "Hammer", Price = 121.50m, CategoryId = 1, ManufacturerId = 1 },
            new Product { Name = "Angle grinder", Price = 1520.00m, CategoryId = 1, ManufacturerId = 1 },
            new Product { Name = "Milk", Price = 14.50m, CategoryId = 3, ManufacturerId = 3 },
            new Product { Name = "Meat", Price = 32.00m, CategoryId = 3, ManufacturerId = 3 },
            new Product { Name = "Bread", Price = 25.50m, CategoryId = 3, ManufacturerId = 3 }
        };
        
        _repository.Setup(x => x.GetProducts()).Returns(fakeproducts);
        var controller = new ProductController(_repository.Object);
        
        // // Act
        var result = controller.Index() as ViewResult;
        
        // // Assert
        CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Product));
        Assert.IsNotNull(result, "View Result is null");
        
        var products = result.ViewData.Model as List<Product>;
        Assert.AreEqual(5, products?.Count, "Got wrong number of products");
    }
    
    [TestMethod]
    public void IndexReturnsViewResultWithListOfProducts()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        
        var products = new List<Product> { new Product(), new Product() };
        _repository.Setup(repo => repo.GetProducts()).Returns(products);

        // Act
        var result = _controller.Index() as ViewResult;

        // Assert
        Assert.IsNotNull(result, "View Result is null");
        var model = result.ViewData.Model as List<Product>;
        Assert.AreEqual(2, model.Count, "Got wrong number of products");
    }
    
    
    
    // SAVE TESTS
    [TestMethod]
    public void SaveIsCalledWhenProductIsCreated()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _repository.Setup(x => x.Save(It.IsAny<Product>()));
        var controller = new ProductController(_repository.Object);
        
        // Act
        var result = controller.Create(new ProductEditViewModel());
        
        // Assert
        _repository.VerifyAll();
        // test på at save er kalt et bestemt antall ganger
        _repository.Verify(x => x.Save(It.IsAny<Product>()), Times.Exactly(1));
    }
    
    
    
    // CREATE TESTS
    [TestMethod]
    public void CreateReturnsViewResultWithProductEditViewModel()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
    
        var productEditViewModel = new ProductEditViewModel();
        _repository.Setup(repo => repo.GetProductEditViewModel()).Returns(productEditViewModel);

        // Act
        var result = _controller.Create() as ViewResult;

        // Assert
        Assert.IsNotNull(result, "View Result is null");
        var model = result.ViewData.Model as ProductEditViewModel;
        Assert.AreEqual(productEditViewModel, model, "Got wrong model");
    }
    
    [TestMethod]
    public void CreatePostReturnsRedirectToActionResultWithValidModel()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        var productEditViewModel = new ProductEditViewModel { Name = "Test Product", Description = "", Price = 14.50m, ManufacturerId = 1, CategoryId = 2 };

        _repository.Setup(repo => repo.Save(It.IsAny<Product>()));
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;

        // Act
        var result = _controller.Create(productEditViewModel) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result, "Action Result is null");
        Assert.AreEqual("Index", result.ActionName, "Redirect to wrong action");
    }

    [TestMethod]
    public void CreatePostReturnsViewResultWhenModelStateIsInvalid()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        var productEditViewModel = new ProductEditViewModel { Name = "Test Product", Description = "", Price = 14.50m, ManufacturerId = 1, CategoryId = 2 };

        _controller.ModelState.AddModelError("error", "some error"); // manually add a model error

        // Act
        var result = _controller.Create(productEditViewModel) as ViewResult;

        // Assert
        Assert.IsNotNull(result, "Action Result is null");
    }

    
    
    
    // EDIT TESTS
    [TestMethod]
    public void EditReturnsViewResultWithProductEditViewModel()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
    
        var productEditViewModel = new ProductEditViewModel();
        _repository.Setup(repo => repo.GetProductEditViewModel(productEditViewModel.Id)).Returns(productEditViewModel);

        // Act
        var result = _controller.Edit(productEditViewModel.Id) as ViewResult;

        // Assert
        Assert.IsNotNull(result, "View Result is null");
        var model = result.ViewData.Model as ProductEditViewModel;
        Assert.AreEqual(productEditViewModel, model, "Got wrong product in the model");
    }
    
    [TestMethod]
    public void EditPostReturnsRedirectToActionResultWithValidModel()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        var productEditViewModel = new ProductEditViewModel { Id = 1, Name = "Test Product", Description = "", Price = 14.50m, ManufacturerId = 1, CategoryId = 2 };

        _repository.Setup(repo => repo.Save(It.IsAny<Product>()));
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;

        // Act
        var result = _controller.Edit(1, productEditViewModel) as RedirectToActionResult;

        // Assert
        Assert.IsNotNull(result, "Action Result is null");
        Assert.AreEqual("Index", result.ActionName, "Redirect to wrong action");
    }
    
    [TestMethod]
    public void EditPostReturnsViewResultWhenExceptionIsThrown()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        var productEditViewModel = new ProductEditViewModel { Id = 1, Name = "Test Product", Description = "", Price = 14.50m, ManufacturerId = 1, CategoryId = 2 };

        _repository.Setup(repo => repo.Save(It.IsAny<Product>())).Throws(new Exception());
        // Act
        var result = _controller.Edit(1, productEditViewModel) as ViewResult;

        // Assert
        Assert.IsNotNull(result, "Action Result is null");
    }
    
    [TestMethod]
    public void EditPostReturnsViewResultWhenModelStateIsInvalid()
    {
        // Arrange
        _repository = new Mock<IProductRepository>();
        _controller = new ProductController(_repository.Object);
        var productEditViewModel = new ProductEditViewModel { Id = 1, Name = "Test Product", Description = "", Price = 14.50m, ManufacturerId = 1, CategoryId = 2 };

        _controller.ModelState.AddModelError("error", "some error"); // manually add a model error

        // Act
        var result = _controller.Edit(1, productEditViewModel) as ViewResult;

        // Assert
        Assert.IsNotNull(result, "Action Result is null");
    }
    
    
    
    // DELETE TESTS
    [TestMethod]
    public void DeleteRedirectToActionResultSuccessfully()
    {
        //Arrange
        _repository = new Mock<IProductRepository>();
        var product = new ProductEditViewModel { Id = 1, Name = "TestHammer", Price = 121.50m, Description = "Verktøy", ManufacturerId = 1, CategoryId = 1};
        _controller = new ProductController(_repository.Object);
        
        _repository.Setup(repo => repo.GetProductEditViewModel(1)).Returns(product);
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;
        
        //Act
        var result = _controller.Delete(1) as RedirectToActionResult;
        
        //Assert
        Assert.AreEqual("Index", result.ActionName, "Redirect to wrong action");
    }
    
    [TestMethod]
    public void DeleteRedirectToActionResultFail()
    {
        //Arrange
        _repository = new Mock<IProductRepository>();
        var product = new ProductEditViewModel { Id = 1, Name = "TestHammer", Price = 121.50m, Description = "Verktøy", ManufacturerId = 1, CategoryId = 1};
        _controller = new ProductController(_repository.Object);
        
        _repository.Setup(repo => repo.GetProductEditViewModel(1)).Returns(product);
        var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        _controller.TempData = tempData;
        
        //Act
        var result = _controller.Delete(2) as RedirectToActionResult; // Trying to delete a non-existing product
        
        //Assert
        Assert.AreEqual("Index", result.ActionName, "Redirect to wrong action");
    }
}