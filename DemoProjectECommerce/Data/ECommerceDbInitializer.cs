using DemoProjectECommerce.productCategory.Static;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace DemoProjectECommerce.productCategory
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


        public static async Task SeedUserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(UserRoles.admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.user))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.user));
                }

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin@ecommerce.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        fullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234!@#$");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.admin);
                }

                string appUserEmail = "user@ecommerce.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        fullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234!@#$");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.user);
                }
            }
        }
    }
}
