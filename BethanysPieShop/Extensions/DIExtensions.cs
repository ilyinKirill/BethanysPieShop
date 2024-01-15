using BethanysPieShop.Models;
using BethanysPieShop.Models.Context;
using BethanysPieShop.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Extensions
{
	public static class DIExtensions
	{
		public static void RegisterRepositories(this IServiceCollection services)
		{
			services.AddScoped<IPieRepository, PieRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		public static void RegisterShoppingCart(this IServiceCollection services)
		{
			services.AddScoped<IShoppingCart, ShoppingCart>(serviceProvider => ShoppingCart.GetCart(serviceProvider));
		}

		public static void RegisterIdentity(this IServiceCollection services)
		{
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<BethanysPieShopDbContext>();
        }

		public static void RegisterDbContext(this IServiceCollection services, WebApplicationBuilder builder, string connectionString)
		{
            builder.Services.AddDbContext<BethanysPieShopDbContext>(options => options.UseSqlServer(connectionString));

        }
    }
}
