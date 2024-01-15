using BethanysPieShop.Models.Context;
using BethanysPieShop.Models.Entities;

namespace BethanysPieShop.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _context;

        public CategoryRepository(BethanysPieShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories => _context.Categories.OrderBy(category => category.Name);
    }
}
