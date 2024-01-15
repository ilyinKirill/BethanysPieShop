using BethanysPieShop.Models.Context;
using BethanysPieShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models.Repositories
{
	public class PieRepository : IPieRepository
	{
		private readonly BethanysPieShopDbContext _context;

		public PieRepository(BethanysPieShopDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Pie> AllPies => _context.Pies.Include(pie => pie.Category);

		public IEnumerable<Pie> PiesOfTheWeek => _context.Pies.Include(pie => pie.Category).Where(pie => pie.IsPieOfTheWeek);

		public Pie? GetPieById(int pieId) => _context.Pies.FirstOrDefault(pie => pie.PieId == pieId);

		public IEnumerable<Pie> SearchPies(string searchQuery)
		{
			return _context.Pies.Where(pie => pie.Name.Contains(searchQuery));
		}
	}
}
