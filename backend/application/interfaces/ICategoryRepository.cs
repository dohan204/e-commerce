using application.cases.Dtos;
using domain.entities;

namespace application.interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryAsync(int id);
        Task<IReadOnlyList<CategoryViewDto>> GetAllCategoriesAsync();
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}