using application.cases.Dtos;
using application.interfaces;
using AutoMapper;
using domain.entities;
using infrastructure.dependency;
using infrastructure.persistence.entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _ctx;
        public CategoryRepository(ApplicationDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryViewDto>> GetAllCategoriesAsync()
        {
            var categories = await _ctx.Categories.ToListAsync();
            var categoriesView = _mapper.Map<IReadOnlyList<CategoryViewDto>>(categories);
            return categoriesView;
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            var category = await _ctx.Categories.FindAsync(id);
            var categoryDo = _mapper.Map<Category>(category);
            // if(category is null) 
            return categoryDo;
        }
        public async Task CreateAsync(Category category)
        {
            var entity = _mapper.Map<CategoryEntity>(category);
            await _ctx.Categories.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            var categoryE = _mapper.Map<CategoryEntity>(category);
            _ctx.Categories.Update(categoryE);
            await _ctx.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _ctx.Categories.FindAsync(id);
            _ctx.Categories.Remove(category);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}