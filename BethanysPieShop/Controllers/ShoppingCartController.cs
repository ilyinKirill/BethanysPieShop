using BethanysPieShop.Models;
using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly IShoppingCart _shoppingCart;

		public ShoppingCartController(IPieRepository pieRepository, IShoppingCart shoppingCart)
		{
			_pieRepository = pieRepository;
			_shoppingCart = shoppingCart;
		}

		public ViewResult Index()
		{
			_shoppingCart.GetShoppingCartItems();
			var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());
			
			return View(shoppingCartViewModel);
		}

		public RedirectToActionResult AddToShoppingCart(int pieId)
		{
			var selectedPie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == pieId);

			if (selectedPie != null)
			{
				_shoppingCart.AddToCart(selectedPie);
			}

			return RedirectToAction("Index");
		}

		public RedirectToActionResult RemoveFromShoppingCart(int pieId)
		{
			var selectedPie = _pieRepository.AllPies.FirstOrDefault(pie => pie.PieId == pieId);

			if (selectedPie != null)
			{
				_shoppingCart.RemoveFromCart(selectedPie);
			}

			return RedirectToAction("Index");
		}
	}
}
