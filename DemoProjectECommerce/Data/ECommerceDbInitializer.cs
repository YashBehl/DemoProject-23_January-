using DemoProjectECommerce.Models.Domain;

namespace DemoProjectECommerce.Data
{
    public class ECommerceDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ECommerceDbContext>();


                context.Database.EnsureCreated();

                if (!context.tbl_Products.Any())
                {
                    context.tbl_Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            productId = Guid.NewGuid(),
                            productName = "ABC earpods",
                            productDescription = "These are wireless earphones",
                            productPrice = 2000,
                            productQuantity = 100,
                            productCategory = Enums.ProductCategory.Electronics
                        },
                        new Product()
                        {
                            productId = Guid.NewGuid(),
                            productName = "Milton Lunch box 1124",
                            productDescription = "Complete Plastic Lunch box",
                            productPrice = 250,
                            productQuantity = 50,
                            productCategory = Enums.ProductCategory.Plastics
                        }
                    });
                }
            }
        }
    }
}
