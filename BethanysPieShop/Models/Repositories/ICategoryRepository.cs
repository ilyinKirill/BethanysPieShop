using BethanysPieShop.Models.Entities;

namespace BethanysPieShop.Models.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
