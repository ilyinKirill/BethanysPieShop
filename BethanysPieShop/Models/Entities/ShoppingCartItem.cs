using System.ComponentModel.DataAnnotations;

namespace BethanysPieShop.Models.Entities
{
	public class ShoppingCartItem
	{
		[Key]
		public int ShoppingCartItemId { get; set; }
		public Pie Pie { get; set; } = default!;
		public int Amount { get; set; }
		public string? ShoppingCartId { get; set; }
	}
}
