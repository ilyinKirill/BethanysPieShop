using BethanysPieShop.Models.Entities;

namespace BethanysPieShop.ViewModels
{
	public class ListViewModel
	{
		public IEnumerable<Pie> Pies { get; }
		public string? CurrentCategory { get; }

		public ListViewModel(IEnumerable<Pie> pies, string? currentCategory)
		{
			Pies = pies;
			CurrentCategory = currentCategory;
		}
	}
}
