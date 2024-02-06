using DemoProjectECommerce.productCategory;
using DemoProjectECommerce.productCategory.Cart;
using DemoProjectECommerce.productCategory.Services;
using DemoProjectECommerce.Models.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ECommerceDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceConnectionString")));

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.getShoppingCart(sc));


//Authentication and authorisation
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ECommerceDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//authentication and authorisation
app.UseAuthentication();
app.UseAuthorization();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

ECommerceDbInitializer.Seed(app);
ECommerceDbInitializer.SeedUserAndRolesAsync(app).Wait();

app.Run();
