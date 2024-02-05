﻿using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectECommerce.Data
{
    public class ECommerceDbContext : IdentityDbContext<AppUser>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public DbSet<Product> tbl_Products { get; set; }
        public DbSet<ShoppingCartItem> tbl_ShoppingCartItems { get; set; }
    }
}
