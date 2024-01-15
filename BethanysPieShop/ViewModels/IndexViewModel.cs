using BethanysPieShop.Models.Entities;

namespace BethanysPieShop.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<Pie> PiesOfTheWeek { get; }

		public IndexViewModel(IEnumerable<Pie> piesOfTheWeek)
		{
			PiesOfTheWeek = piesOfTheWeek;
		}
	}
}
