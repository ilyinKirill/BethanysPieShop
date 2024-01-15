using BethanysPieShop.Extensions;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BethanysPieShopDbContextConnection' not found.");

// Add servisec to the container
builder.Services.RegisterDbContext(builder, connectionString);
builder.Services.RegisterIdentity();
builder.Services.RegisterRepositories();
builder.Services.RegisterShoppingCart();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();
app.UseSession();
app.MapDefaultControllerRoute();
app.MapFallbackToPage("/app/{catchall}", "/App/Index");
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

DbInitializer.Seed(app);

app.Run();