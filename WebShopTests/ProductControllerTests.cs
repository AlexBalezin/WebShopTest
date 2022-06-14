using Moq;
using WebShop.Models;
using Xunit;
using System.Linq;
using WebShop.Controllers;
using System.Collections.Generic;
using WebShop.Models.ViewModels;

namespace WebShopTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"},
                new Product { Id = 3, Name = "P3"},
                new Product { Id = 4, Name = "P4"},
                new Product { Id = 5, Name = "P5"}
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }
    }
}