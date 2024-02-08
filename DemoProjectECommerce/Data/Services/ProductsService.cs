using DemoProjectECommerce.productCategory.Base;
using DemoProjectECommerce.productCategory.ViewModels;
using DemoProjectECommerce.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DemoProjectECommerce.productCategory.Enums;

namespace DemoProjectECommerce.productCategory.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly ECommerceDbContext _context;
        public ProductsService(ECommerceDbContext context):base(context)
        {
            _context = context;
        }

        public async Task addNewProductAsync(NewProductViewModel data)
        {
            var newProduct = new Product()
            {
                productName = data.productName,
                productDescription = data.productDescription,
                productPrice = data.productPrice,
                productImageUrl = data.productImageUrl,
                productQuantity = data.productQuantity,
                productCategory = data.productCategory,
                createdAt = data.createdAt
            };
            await _context.tbl_Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> getProductByIdAsync(Guid id)
        {
            var productDetails = await _context.tbl_Products.FirstOrDefaultAsync(n => n.productId == id);
            return productDetails;
        }

        public async Task UpdateProductAsync(NewProductViewModel data)
        {
            var dbProduct = await _context.tbl_Products.FirstOrDefaultAsync(n => n.productId == data.productId);

            if (dbProduct != null)
            {
                dbProduct.productName = data.productName;
                dbProduct.productDescription = data.productDescription;
                dbProduct.productPrice = data.productPrice;
                dbProduct.productImageUrl = data.productImageUrl;
                dbProduct.productQuantity = data.productQuantity;
                dbProduct.productCategory = data.productCategory;
                dbProduct.createdAt = data.createdAt;
                dbProduct.isHot = data.isHot;
                dbProduct.isTrending = data.isTrending;
                dbProduct.isUnavailable = data.isUnavailable;
                await _context.SaveChangesAsync();
            }
            
            await _context.SaveChangesAsync();
        }
    }
}
