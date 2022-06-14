using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 4;

        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int productPage = 1) 
            => View(repository.Products
                .OrderBy(p=>p.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize));
    }
}
