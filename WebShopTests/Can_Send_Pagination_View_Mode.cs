using Moq;
using WebShop.Models;
using Xunit;
using System.Linq;
using WebShop.Controllers;
using System.Collections.Generic;
using WebShop.Models.ViewModels;

namespace WebShopTests
{
    public class Can_Send_Pagination_View_Mode
    {
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            //Arrange
            mock.Setup(m => m.Products).Returns((new Product[]
                {
                    new Product { Id = 1, Name = "P1"},
                    new Product { Id = 2, Name = "P2"},
                    new Product { Id = 3, Name = "P3"},
                    new Product { Id = 4, Name = "P4"},
                    new Product { Id = 5, Name = "P5"},
                }).AsQueryable());

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            //Act 
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            //Assert
            PagingInfo pagelnfo = result.PagingInfo;
            Assert.Equal(2, pagelnfo.CurrentPage);
            Assert.Equal(3, pagelnfo.ItemsPerPage);
            Assert.Equal(5, pagelnfo.TotalItems);
            Assert.Equal(2, pagelnfo.TotalPages);
        }
    }
}
