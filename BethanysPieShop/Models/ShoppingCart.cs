using BethanysPieShop.Models.Context;
using BethanysPieShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
	public class ShoppingCart : IShoppingCart
	{
		private readonly BethanysPieShopDbContext _context;
		public string? ShoppingCartId { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

		private ShoppingCart(BethanysPieShopDbContext context)
		{
			_context = context;
		}

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
			BethanysPieShopDbContext context = services.GetService<BethanysPieShopDbContext>() ?? throw new Exception("Error initializing");
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
			session?.SetString("CartId", cartId);

			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}

		public void AddToCart(Pie pie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(cartItem => cartItem.Pie.PieId == pie.PieId && cartItem.ShoppingCartId == ShoppingCartId);

			if (shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = ShoppingCartId,
					Pie = pie,
					Amount = 1
				};

				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				shoppingCartItem.Amount++;
			}

			_context.SaveChanges();
		}

		public void ClearCart()
		{
			var cartItems = _context.ShoppingCartItems.Where(cartItem => cartItem.ShoppingCartId == ShoppingCartId);
			_context.ShoppingCartItems.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??= _context.ShoppingCartItems.Where(cartItem => cartItem.ShoppingCartId == ShoppingCartId).Include(cartItem => cartItem.Pie).ToList();
		}

		public decimal GetShoppingCartTotal()
		{
			var total = _context.ShoppingCartItems.Where(cartItem => cartItem.ShoppingCartId == ShoppingCartId).Select(cartItem => cartItem.Amount * cartItem.Pie.Price).Sum();

			return total;
		}

		public int RemoveFromCart(Pie pie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(cartItem => cartItem.Pie.PieId == pie.PieId && cartItem.ShoppingCartId == ShoppingCartId);
			var localAmount = 0;

			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();

			return localAmount;
		}
	}
}
