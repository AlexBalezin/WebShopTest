﻿namespace WebShop.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDbContext context;

        public EFProductRepository(ApplicationDbContext context)
        {
            this.context = context;  
        }

        public IQueryable<Product> Products => context.Products;
    }
}
