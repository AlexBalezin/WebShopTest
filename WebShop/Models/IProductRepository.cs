namespace WebShop.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
