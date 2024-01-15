using BethanysPieShop.Models.Context;
using BethanysPieShop.Models.Entities;

namespace BethanysPieShop.Models.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly BethanysPieShopDbContext _context;
		private IShoppingCart _shoppingCart;

		public OrderRepository(BethanysPieShopDbContext context, IShoppingCart shoppingCart)
		{
			_context = context;
			_shoppingCart = shoppingCart;
		}

		public void CreateOrder(Order order)
		{
			order.OrderPlaced = DateTime.Now;

			List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
			order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

			order.OrderDetails = new List<OrderDetail>();

			foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
			{
				var orderDetail = new OrderDetail
				{
					Amount = shoppingCartItem.Amount,
					PieId = shoppingCartItem.Pie.PieId,
					Price = shoppingCartItem.Pie.Price
				};

				order.OrderDetails.Add(orderDetail);
			}

			_context.Orders.Add(order);

			_context.SaveChanges();
		}
	}
}
