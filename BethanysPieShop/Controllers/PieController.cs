using BethanysPieShop.Models.Entities;
using BethanysPieShop.Models.Repositories;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
	public class PieController : Controller
	{
		private readonly IPieRepository _pieRepository;
		private readonly ICategoryRepository _categoryRepository;

		public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
		{
			_pieRepository = pieRepository;
			_categoryRepository = categoryRepository;
		}

		public IActionResult List(string category)
		{
			IEnumerable<Pie> pies;
			string? currentCategory;

			if (string.IsNullOrEmpty(category))
			{
				pies = _pieRepository.AllPies.OrderBy(pie => pie.PieId);
				currentCategory = "All pies";
			}
			else
			{
				pies = _pieRepository.AllPies.Where(pie => pie.Category.Name == category).OrderBy(pie => pie.PieId);
				currentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.Name == category)?.Name;
			}

			var listViewModel = new ListViewModel(pies, currentCategory);

			return View(listViewModel);
		}

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
			return pie == null ? NotFound() : View(pie);
        }
    }
}
