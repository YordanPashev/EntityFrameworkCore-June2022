namespace FastFood.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FastFood.Data;
    using FastFood.Models;
    using Models.Categories;
    using Microsoft.EntityFrameworkCore;
    using Services.Interfaces;
    using System.Collections.Generic;

    public class CategoryService : ICategoryService
    {
        private FastFoodContext dbContext;
        private readonly IMapper mapper;

        public CategoryService(FastFoodContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task Add(CreateCategoryDTO categoryDTO)
        {
            Category category = this.mapper.Map<Category>(categoryDTO);

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<ListCategoryDTOs>> GetAll()
        {
            ICollection<ListCategoryDTOs> result = await this.dbContext.Categories
                .ProjectTo<ListCategoryDTOs>(this.mapper.ConfigurationProvider)
                .ToArrayAsync();

            return result;
        }
    }
}
